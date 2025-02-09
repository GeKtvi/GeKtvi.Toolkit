namespace GeKtvi.Toolkit.Tests;

[TestClass]
public class AppConfigHelperTests
{
    private static readonly TestConfig Config = new()
    {
        Property1 = Random.Shared.Next(0, 101),
        Property2 = new ComparableList<int>( Enumerable.Repeat(0, Random.Shared.Next(0, 200))
            .Select(x => Random.Shared.Next())
            .ToArray()),
        Property3 = "MyAwesomeString"
    };

    [TestMethod]
    public void WriteConfig_ConfigObject_CorrectSavedConfig() => WriteConfig(SerializerType.Xml);

    [TestMethod]
    public void WriteConfig_ConfigObjectJson_CorrectSavedConfig() => WriteConfig(SerializerType.Json);

    [TestMethod]
    public void LoadConfig_SavedConfig_CorrectLoadedConfig() => TestLoadConfig(SerializerType.Xml);

    [TestMethod]
    public void LoadConfig_SavedConfigJson_CorrectLoadedConfig() => TestLoadConfig(SerializerType.Json);

    private static void TestLoadConfig(SerializerType serializerType)
    {
        WriteConfig(serializerType);

        var config = AppConfigHelper.LoadConfig<TestConfig>(serializerType);
        Assert.AreEqual(Config, config);
    }

    private static void WriteConfig(SerializerType serializerType) => AppConfigHelper.WriteConfig(Config, serializerType);

    public record TestConfig
    {
        public int Property1 { get; set; }
        public ComparableList<int> Property2 { get; set; } = [];
        public string Property3 { get; set; } = string.Empty;
    }

    public class ComparableList<T> : List<T>
    {
        public ComparableList()
        {
        }

        public ComparableList(IEnumerable<T> collection) : base(collection)
        {
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            if (obj is IEnumerable<T> enumerable)
                return Enumerable.SequenceEqual(this, enumerable);

            return false;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}
