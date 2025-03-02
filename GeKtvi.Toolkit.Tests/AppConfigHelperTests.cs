namespace GeKtvi.Toolkit.Tests;

[TestClass]
public class AppConfigHelperTests
{
    private static readonly TestConfig _config = TestConfig.CurrentRandom;  

    [TestMethod]
    public void WriteConfig_ConfigObject_CorrectSavedConfig() => WriteConfig(SerializerType.Xml);

    [TestMethod]
    public void WriteConfig_ConfigObjectJson_CorrectSavedConfig() => WriteConfig(SerializerType.Json);

    [TestMethod]
    public void LoadConfig_SavedConfig_CorrectLoadedConfig() => TestLoadConfig(SerializerType.Xml);

    [TestMethod]
    public void LoadConfig_SavedConfigJson_CorrectLoadedConfig() => TestLoadConfig(SerializerType.Json);

    [TestCleanup]
    public void Cleanup() => Directory.Delete("Configs", true);

    private static void TestLoadConfig(SerializerType serializerType)
    {
        WriteConfig(serializerType);

        var config = AppConfigHelper.LoadConfig<TestConfig>(serializerType);
        Assert.AreEqual(_config, config);
    }

    private static void WriteConfig(SerializerType serializerType) => AppConfigHelper.WriteConfig(_config, serializerType);
}
