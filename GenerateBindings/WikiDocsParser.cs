using System.Text;
using System.Xml;
using Markdig;
using Markdig.Extensions.Tables;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace GenerateBindings;

public class WikiDocsParser(DirectoryInfo? basePath)
{
    private const string SDL_WIKI_PREFIX = "\u0006WIKISTART\u0006";
    private const string SDL_WIKI_SUFFIX = "\u0006WIKIEND\u0006";
    private readonly List<(string name, bool formatIndented, Task<string?> task)> _wikiContents = [];
    private readonly Dictionary<string, string> _csharpContainingTypes = [];

    /// <summary>
    /// Register how a member can be referenced in C#. E.g. SDL_INIT_TIMER belongs in SDL_InitFlags.
    /// </summary>
    public void RegisterContainingType(string? memberName, string? containingType)
    {
        if (!string.IsNullOrEmpty(memberName) && !string.IsNullOrEmpty(containingType))
        {
            _csharpContainingTypes.Add(memberName, containingType);
        }
    }

    public void Preload(string? name, StringBuilder output, bool formatIndented = true)
    {
        if (basePath == null) return;
        if (string.IsNullOrEmpty(name)) return;

        _wikiContents.Add((name, formatIndented, PreloadImpl(name)));
        output.Append(SDL_WIKI_PREFIX).Append(name).Append(SDL_WIKI_SUFFIX);
    }

    private async Task<string?> PreloadImpl(string entry)
    {
        if (basePath == null) return null;
        try
        {
            var file = Path.Combine(basePath.FullName, "SDL3", entry + ".md");
            if (!File.Exists(file)) return null;
            return await File.ReadAllTextAsync(file);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public void ProcessDocComments(StringBuilder output)
    {
        var docComments = new (string name, string docComment)[_wikiContents.Count];
        for (var i = 0; i < _wikiContents.Count; i++)
        {
            var entry = _wikiContents[i];
            string docComment;
            try
            {
                docComment = ProcessDocComment(entry.name, entry.formatIndented, entry.task.Result);
            }
            catch (Exception)
            {
                docComment = "";
            }

            if (docComment.Length > 0)
            {
                var docCommentSpan = docComment.AsSpan();
                var commented = new StringBuilder();
                while (docCommentSpan.Length > 0)
                {
                    var index = docCommentSpan.IndexOf('\n');
                    commented.Append("/// ").Append(index < 0 ? docCommentSpan : docCommentSpan[..index]).Append('\n');

                    if (index >= 0)
                    {
                        docCommentSpan = docCommentSpan[(index + 1)..];
                    }
                    else
                    {
                        break;
                    }
                }

                docComment = commented.ToString();
            }

            docComments[i] = (entry.name, docComment);
        }

        foreach (var (name, comment) in docComments)
        {
            output.Replace(SDL_WIKI_PREFIX + name + SDL_WIKI_SUFFIX, comment);
        }

        Console.WriteLine($"Finished generating docs for {docComments.Length} entries");
    }

    private string ProcessDocComment(string name, bool formatIndented, string? wikiContentsRaw)
    {
        if (wikiContentsRaw == null) return "";

        var wikiContentsSpan = wikiContentsRaw.AsSpan();
        var markdownPipeline = new MarkdownPipelineBuilder().UsePipeTables().UsePreciseSourceLocation().Build();
        var markdown = Markdown.Parse(wikiContentsRaw, markdownPipeline);

        var doc = new XmlDocument();
        var result = doc.CreateElement("root");
        var docMisc = doc.CreateElement("misc");
        docMisc.AppendChild(doc.CreateTextNode($"https://wiki.libsdl.org/SDL3/{name}"));
        var seeAlsos = new List<XmlElement>();
        var docComment = doc.CreateElement("summary");
        result.AppendChild(docComment);

        ReadOnlySpan<char> lastHeading = [];
        var parseMode = ParseMode.NotParsing;
        foreach (var topLevelBlock in markdown)
        {
            if (topLevelBlock is HeadingBlock headingBlock)
            {
                if (headingBlock.Level <= 1)
                {
                    parseMode = ParseMode.Main;
                }

                if (parseMode != ParseMode.NotParsing)
                {
                    parseMode = ParseMode.Main;
                }

                if (parseMode != ParseMode.NotParsing)
                {
                    var content = headingBlock.Inline;
                    var nextHeading = content == null ? [] : ToString(content, wikiContentsSpan);

                    if (!lastHeading.SequenceEqual(nextHeading))
                    {
                        CreateTopLevelXmlElement(nextHeading);
                    }
                }
            }
            else if (parseMode != ParseMode.NotParsing)
            {
                var tableHadHeader = false;
                string? lastParamName = null;
                var isProcessingHeaderRow = false;
                var columnIndex = -1;
                ProcessBlock(docComment, topLevelBlock, wikiContentsSpan);

                void ProcessBlock(XmlElement parent, MarkdownObject? block, ReadOnlySpan<char> wikiContentsSpan)
                {
                    if (block == null) return;

                    if (block is ContainerInline containerInline)
                    {
                        XmlElement? seeElement = null;
                        if (block is LinkInline linkInline)
                        {
                            seeElement = doc.CreateElement("see");
                            seeElement.SetAttribute("href", linkInline.Url);
                            parent.AppendChild(seeElement);
                            parent = seeElement;
                        }

                        foreach (var subBlock in containerInline)
                        {
                            ProcessBlock(parent, subBlock, wikiContentsSpan);
                        }

                        if (seeElement != null)
                        {
                            var inner = seeElement.InnerText;
                            var href = seeElement.GetAttribute("href");
                            if (inner == href && inner.StartsWith("SDL_"))
                            {
                                seeElement.RemoveAll();
                                if (_csharpContainingTypes.TryGetValue(inner, out var containingType))
                                {
                                    seeElement.SetAttribute("cref", containingType + '.' + inner);
                                }
                                else
                                {
                                    seeElement.SetAttribute("cref", inner);
                                }

                                seeElement.IsEmpty = true;
                            }
                            else if (!href.Contains('/'))
                            {
                                seeElement.SetAttribute("href", $"https://wiki.libsdl.org/SDL3/{href}");
                            }

                            if (parseMode == ParseMode.SeeAlso)
                            {
                                var seeAlso = doc.CreateElement("seealso");
                                if (seeElement.HasAttribute("cref"))
                                {
                                    seeAlso.SetAttribute("cref", seeElement.GetAttribute("cref"));
                                }
                                else
                                {
                                    seeAlso.SetAttribute("href", seeElement.GetAttribute("href"));
                                    seeAlso.InnerXml = seeElement.InnerXml;
                                }

                                seeAlsos.Add(seeAlso);
                                seeElement.ParentNode!.RemoveChild(seeElement);
                            }
                        }
                    }
                    else if (block is ContainerBlock containerBlock)
                    {
                        XmlElement? paramNameElem = null;
                        if (block is QuoteBlock)
                        {
                            var code = doc.CreateElement("code");
                            parent.AppendChild(code);
                            parent = code;
                        }
                        else if (block is Table)
                        {
                            tableHadHeader = false;
                            if (parseMode != ParseMode.FunctionParams)
                            {
                                var tableElem = doc.CreateElement("list");
                                tableElem.SetAttribute("type", "table");
                                parent.AppendChild(tableElem);
                                parent = tableElem;
                            }
                            else
                            {
                                lastParamName = null;
                            }
                        }
                        else if (block is TableRow tableRow)
                        {
                            columnIndex = -1;
                            if (parseMode != ParseMode.FunctionParams)
                            {
                                if (tableRow.IsHeader)
                                {
                                    tableHadHeader = true;
                                }
                                else if (!tableHadHeader)
                                {
                                    var headerElem = doc.CreateElement("listheader");
                                    parent.AppendChild(headerElem);

                                    for (var i = 0; i < tableRow.Count; i++)
                                    {
                                        var cellElem = doc.CreateElement("term");
                                        cellElem.InnerText = i.ToString();
                                        headerElem.AppendChild(cellElem);
                                    }
                                }

                                isProcessingHeaderRow = tableRow.IsHeader;
                                var rowElem = doc.CreateElement(tableRow.IsHeader ? "listheader" : "item");
                                parent.AppendChild(rowElem);
                                parent = rowElem;
                            }
                        }
                        else if (block is TableCell)
                        {
                            columnIndex++;
                            if (parseMode != ParseMode.FunctionParams)
                            {
                                var rowElem = doc.CreateElement(isProcessingHeaderRow ? "term" : "description");
                                parent.AppendChild(rowElem);
                                parent = rowElem;
                            }
                            else
                            {
                                // column 0 = type
                                // column 1 = param name
                                // column 2 = param description
                                if (columnIndex == 0) return;
                                if (columnIndex == 1)
                                {
                                    paramNameElem = doc.CreateElement("paramname");
                                    parent = paramNameElem;
                                }
                                else if (!string.IsNullOrEmpty(lastParamName))
                                {
                                    var paramElem = doc.CreateElement("param");
                                    paramElem.SetAttribute("name", lastParamName);
                                    result.AppendChild(paramElem);
                                    parent = paramElem;
                                }
                            }
                        }
                        else if (block is ListBlock)
                        {
                            if (parseMode != ParseMode.SeeAlso)
                            {
                                var listElem = doc.CreateElement("list");
                                listElem.SetAttribute("type", "bullet");
                                parent.AppendChild(listElem);
                                parent = listElem;
                            }
                        }
                        else if (block is ListItemBlock)
                        {
                            if (parseMode != ParseMode.SeeAlso)
                            {
                                var itemElem = doc.CreateElement("item");
                                parent.AppendChild(itemElem);
                                var descElem = doc.CreateElement("description");
                                itemElem.AppendChild(descElem);
                                parent = descElem;
                            }
                        }

                        foreach (var subBlock in containerBlock)
                        {
                            ProcessBlock(parent, subBlock, wikiContentsSpan);
                        }

                        if (paramNameElem != null)
                        {
                            lastParamName = paramNameElem.InnerText.Trim();
                            paramNameElem.RemoveAll();
                        }
                    }
                    else if (block is LeafBlock leafBlock)
                    {
                        if (block is ParagraphBlock)
                        {
                            if (parseMode != ParseMode.SeeAlso)
                            {
                                var para = doc.CreateElement("para");
                                parent.AppendChild(para);
                                parent = para;
                            }
                        }
                        else if (block is CodeBlock codeBlock)
                        {
                            var code = doc.CreateElement(codeBlock is FencedCodeBlock ? "code" : "c");
                            parent.AppendChild(code);
                            parent = code;

                            for (var i = 0; i < codeBlock.Lines.Count; i++)
                            {
                                var line = codeBlock.Lines.Lines[i];
                                code.AppendChild(doc.CreateTextNode($"{line}\n"));
                            }
                        }
                        else if (block is ThematicBreakBlock)
                        {
                            parseMode = ParseMode.Main;
                            docMisc.AppendChild(doc.CreateElement("br"));
                            docMisc.AppendChild(doc.CreateElement("br"));
                            docComment = docMisc;
                        }

                        ProcessBlock(parent, leafBlock.Inline, wikiContentsSpan);
                    }
                    else if (block is LineBreakInline)
                    {
                        // Ignored
                    }
                    else if (block is CodeInline codeInline)
                    {
                        var code = doc.CreateElement("c");
                        parent.AppendChild(code);
                        var text = doc.CreateTextNode(codeInline.Content);
                        code.AppendChild(text);
                    }
                    else
                    {
                        var text = doc.CreateTextNode($"{ToString(block, wikiContentsSpan)}");
                        parent.AppendChild(text);
                    }
                }
            }
        }

        foreach (var child in seeAlsos)
        {
            result.AppendChild(child);
        }

        result.AppendChild(docMisc);

        using var stream = new MemoryStream();
        using var writer = new XmlTextWriter(stream, Encoding.Unicode);
        writer.Formatting = formatIndented ? Formatting.Indented : Formatting.None;
        result.WriteContentTo(writer);
        writer.Flush();
        stream.Flush();
        stream.Position = 0;
        var reader = new StreamReader(stream);
        return reader.ReadToEnd();

        void CreateTopLevelXmlElement(ReadOnlySpan<char> heading)
        {
            if (heading.SequenceEqual(name))
            {
                // ignored
            }
            else if (parseMode == ParseMode.FunctionParams && !heading.IsEmpty)
            {
                var param = doc.CreateElement("param");
                param.SetAttribute("name", new string(heading));
                docComment = param;
                result.AppendChild(docComment);
            }
            else if (heading is "Function Parameters")
            {
                parseMode = ParseMode.FunctionParams;
            }
            else if (heading is "Return Value")
            {
                docComment = doc.CreateElement("returns");
                result.AppendChild(docComment);
            }
            else if (heading is "Remarks")
            {
                docComment = doc.CreateElement("remarks");
                result.AppendChild(docComment);
            }
            else if (heading is "See Also")
            {
                parseMode = ParseMode.SeeAlso;
            }
            else if (parseMode == ParseMode.Main && !heading.IsEmpty)
            {
                docMisc.AppendChild(doc.CreateElement("br"));
                var miscHeader = doc.CreateElement("b");
                miscHeader.InnerText = $"## {heading} ##";
                docMisc.AppendChild(miscHeader);

                docComment = docMisc;
            }
        }
    }

    private enum ParseMode
    {
        NotParsing,
        Main,
        FunctionParams,
        SeeAlso,
    }

    private static ReadOnlySpan<char> ToString(MarkdownObject block, ReadOnlySpan<char> wikiContentsSpan)
    {
        var span = block.Span;
        return wikiContentsSpan[span.Start..(span.End + 1)];
    }
}
