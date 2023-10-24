using System;
using System.IO;
using System.Xml.Serialization;

namespace GeKtvi.Toolkit
{
    public class SettingsManager<SettingsType>
    {
        private readonly string _saveFileName;
        private readonly Func<SettingsType> _settingsFactory;
        private SettingsType? _settings;

        public SettingsManager(string folder, Func<SettingsType> settingsFactory, string? folderDirectory = null, string? fileName = null)
        {
            folderDirectory ??= Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            fileName ??= typeof(SettingsType).ToString() + ".save";

            _saveFileName = $@"{folderDirectory}\{folder}\{fileName}";

            _settingsFactory = settingsFactory;
        }

        public SettingsType Load()
        {
            try
            {
                using FileStream fs = new(_saveFileName, FileMode.OpenOrCreate);
                _settings = (SettingsType)(new XmlSerializer(typeof(SettingsType)).Deserialize(fs)
                    ?? throw new InvalidDataException($"Cant cast serialized object to {nameof(SettingsType)}"));
            }
            catch (IOException)
            {
                _settings = _settingsFactory.Invoke();
            }
            return _settings;
        }

        public void Save()
        {
            using FileStream fs = new(_saveFileName, FileMode.OpenOrCreate);
            fs.SetLength(0);
            new XmlSerializer(typeof(SettingsType)).Serialize(fs, _settings);
        }
    }
}
