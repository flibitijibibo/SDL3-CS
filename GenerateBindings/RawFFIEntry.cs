using System.Text.Json.Serialization;

namespace GenerateBindings;

internal class RawFFIEntry
{
    [JsonPropertyName("tag")]
    public string Tag { get; }
    [JsonPropertyName("name")]
    public string? Name { get; }
    [JsonPropertyName("location")]
    public string? Header { get; }
    [JsonPropertyName("fields")]
    public RawFFIEntry[]? Fields { get; }
    [JsonPropertyName("value")]
    public uint? Value { get; }

    [JsonConstructor]
    public RawFFIEntry(string tag, string? name, string? header, RawFFIEntry[]? fields, uint? value)
    {
        Tag = tag;
        Name = name;
        Header = header;
        Fields = fields;
        Value = value;
    }
}
