namespace GeKtvi.Toolkit.Tests
{
    [TestClass]
    public class SettingsManagerTests
    {
        private const string AppName = nameof(SettingsManagerTests);
        private const string ChangedValue = "ChangedValue";
        private const string ChangedValue2 = "ChangedValue2";
        private const string FolderDirectory = "TestSettings";
        private bool _isFactoryExecuted;

        [TestMethod]
        public void Test_XmlSettingsManager_CorrectWorkedManager() => Test(CreateXmlSettingManager());

        [TestMethod]
        public void Test_JsonSettingsManager_CorrectWorkedManager() => Test(CreateJsonSettingManager());

        public void Test(ISettingsManager<TestConfig> manager)
        {
            _isFactoryExecuted = false;

            var settings = manager.TryLoad();

            Assert.IsTrue(_isFactoryExecuted);
            Assert.IsNotNull(settings);
            Assert.AreEqual(ChangedValue, settings.Property3);
            Assert.AreEqual(TestConfig.CurrentRandom, settings);

            settings.Property1 = -1;
            manager.Save();

            _isFactoryExecuted = false;

            var settings2 = manager.Load();

            Assert.AreEqual(settings, settings2);

            manager.Dispose();
        }

        private ISettingsManager<TestConfig> CreateXmlSettingManager() => 
            new SettingsManager<TestConfig>(
                AppName,
                () =>
                {
                    _isFactoryExecuted = true;
                    return TestConfig.CurrentRandom;
                },
                FolderDirectory,
                "MySettings.save",
                s => s.Property3 = ChangedValue
            );
        
        private ISettingsManager<TestConfig> CreateJsonSettingManager() => 
            new JsonSettingsManager<TestConfig>(
                AppName,
                () =>
                {
                    _isFactoryExecuted = true;
                    return TestConfig.CurrentRandom;
                },
                FolderDirectory,
                "MySettings.save",
                s => s.Property3 = ChangedValue
            );

        [TestCleanup]
        public void Cleanup() => Directory.Delete(FolderDirectory, true);
    }
}
