using System.Xml.Serialization;

namespace GeKtvi.Toolkit.Tests;

public record TestConfig
{
    public int Property1 { get; set; }
    public ComparableList<int> Property2 { get; set; } = [];
    public string Property3 { get; set; } = string.Empty;

    [XmlIgnore]
    public ComparableDictionary<string, string>? Property4 { get; set; } = null;

    public static readonly TestConfig CurrentRandom = CreateCurrentRandom();
    public static readonly TestConfig CurrentRandomWithDictionary = CreateCurrentRandom(true);

    public static TestConfig GetCurrentRandom(SerializerType serializerType) =>
        serializerType switch
        {
            SerializerType.Xml => CurrentRandom,
            SerializerType.Json => CurrentRandomWithDictionary,
            _ => throw new ArgumentException("Not supported", nameof(serializerType)),
        };

    private static TestConfig CreateCurrentRandom(bool addDictionary = false) => 
        new()
        {
            Property1 = Random.Shared.Next(0, 101),
            Property2 = new ComparableList<int>(Enumerable.Repeat(0, Random.Shared.Next(0, 200))
            .Select(x => Random.Shared.Next())
            .ToArray()),
            Property3 = "MyAwesomeString",
            Property4 = addDictionary ?
            new ComparableDictionary<string, string>()
            {
                ["stringKey1"] = "stringValue1",
                ["stringKey2"] = "stringValue2",
                ["stringKey3"] = "stringValue3",
            }
            : null
        };
}

