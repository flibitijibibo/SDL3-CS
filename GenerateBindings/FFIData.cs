using System.Text.Json.Serialization;

namespace GenerateBindings;

internal class FFIData
{
    [JsonPropertyName("enums")]
    public IReadOnlyDictionary<string, Enum> Enums { get; }

    [JsonPropertyName("records")]
    public IReadOnlyDictionary<string, Record> Records { get; }

    [JsonPropertyName("functions")]
    public IReadOnlyDictionary<string, CFunction> Functions { get; }

    [JsonConstructor]
    public FFIData(
        IReadOnlyDictionary<string, Enum> enums,
        IReadOnlyDictionary<string, Record> records,
        IReadOnlyDictionary<string, CFunction> functions
    )
    {
        Enums = enums;
        Records = records;
        Functions = functions;
    }
}

internal class Enum
{
    [JsonPropertyName("values")]
    public EnumValue[] Values { get; }

    [JsonConstructor]
    public Enum(EnumValue[] values) { Values = values; }
}

internal class EnumValue
{
    [JsonPropertyName("name")]
    public string Name { get; }

    [JsonPropertyName("value")]
    public int Value { get; }

    [JsonConstructor]
    public EnumValue(string name, int value)
    {
        Name = name;
        Value = value;
    }
}

internal class Record
{
    [JsonPropertyName("fields")]
    public Field[] Fields { get; }

    [JsonPropertyName("comment")]
    public string Comment { get; }

    [JsonConstructor]
    public Record(Field[] fields, string comment)
    {
        Fields = fields;
        Comment = comment;
    }
}

internal class Field
{
    [JsonPropertyName("name")]
    public string Name { get; }

    [JsonPropertyName("type")]
    public Type Type { get; }

    [JsonConstructor]
    public Field(string name, Type type)
    {
        Name = name;
        Type = type;
    }
}

internal class CFunction
{
    [JsonPropertyName("return_type")]
    public Type ReturnType { get; }

    [JsonPropertyName("parameters")]
    public Parameter[] Parameters { get; }

    [JsonPropertyName("comment")]
    public string Comment { get; }

    [JsonConstructor]
    public CFunction(Type returnType, Parameter[] parameters, string comment)
    {
        ReturnType = returnType;
        Parameters = parameters;
        Comment = comment;
    }
}

internal class Parameter
{
    [JsonPropertyName("name")]
    public string Name { get; }

    [JsonPropertyName("type")]
    public Type Type { get; }

    [JsonConstructor]
    public Parameter(string name, Type type)
    {
        Name = name;
        Type = type;
    }
}

internal class Type
{
    [JsonPropertyName("name")]
    public string Name { get; }
    [JsonPropertyName("kind")]
    public string Kind { get; }

    [JsonConstructor]
    public Type(string name, string kind)
    {
        Name = name;
        Kind = kind;
    }
}
