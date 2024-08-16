using System.Text.Json.Serialization;

namespace GenerateBindings;

internal class FFIData
{
    [JsonPropertyName("enums")]
    public IReadOnlyDictionary<string, Enum> Enums { get; }

    [JsonConstructor]
    public FFIData(IReadOnlyDictionary<string, Enum> enums) { Enums = enums; }
}

internal class Enum
{
    [JsonPropertyName("size_of")]
    public int SizeOf { get; }
    [JsonPropertyName("values")]
    public EnumValue[] Values { get; }

    [JsonConstructor]
    public Enum(int sizeOf, EnumValue[] values)
    {
        SizeOf = sizeOf;
        Values = values;
    }
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
