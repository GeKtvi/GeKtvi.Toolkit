namespace GeKtvi.Toolkit.Tests;

public record TestConfig
{
    public int Property1 { get; set; }
    public ComparableList<int> Property2 { get; set; } = [];
    public string Property3 { get; set; } = string.Empty;

    public static readonly TestConfig CurrentRandom = new()
    {
        Property1 = Random.Shared.Next(0, 101),
        Property2 = new ComparableList<int>(Enumerable.Repeat(0, Random.Shared.Next(0, 200))
        .Select(x => Random.Shared.Next())
        .ToArray()),
        Property3 = "MyAwesomeString"
    };
}

