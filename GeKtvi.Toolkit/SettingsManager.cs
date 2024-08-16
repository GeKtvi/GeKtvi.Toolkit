using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Xml.Serialization;

namespace GeKtvi.Toolkit
{
    public class SettingsManager<SettingsType>
    {
        public event EventHandler<SettingsType>? AfterLoad;

        private readonly string _saveFileName;
        private readonly string _saveDirectory;
        private readonly Func<SettingsType> _settingsFactory;
        private SettingsType? _settings;

        public SettingsManager(
            string folder,
            Func<SettingsType> settingsFactory,
            string? folderDirectory = null,
            string? fileName = null,
            Action<SettingsType>? afterLoad = null)
        {
            folderDirectory ??= Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            fileName ??= typeof(SettingsType).Name + ".save";

            _saveDirectory = $@"{folderDirectory}\{folder}";
            _saveFileName = $@"{_saveDirectory}\{fileName}";

            _settingsFactory = settingsFactory;

            if (afterLoad is not null)
                AfterLoad += (s, e) => afterLoad(e);
        }

        public SettingsType TryLoad()
        {
            try
            {
                LoadFile();
            }
            catch (IOException)
            {
                _settings = _settingsFactory.Invoke();
            }
            catch (InvalidOperationException)
            {
                _settings = _settingsFactory.Invoke();
            }
            AfterLoad?.Invoke(this, _settings);
            return _settings;
        }

        public SettingsType Load()
        {
            LoadFile();
            AfterLoad?.Invoke(this, _settings);
            return _settings;
        }

#if NET6_0_OR_GREATER
        [MemberNotNull(nameof(_settings))]
#endif
        private void LoadFile()
        {
            using FileStream fs = new(_saveFileName, FileMode.OpenOrCreate);
            _settings = (SettingsType)(new XmlSerializer(typeof(SettingsType)).Deserialize(fs)
                ?? throw new InvalidDataException($"Cant cast serialized object to {nameof(SettingsType)}"));
        }

        public void Save()
        {
            Directory.CreateDirectory(_saveDirectory);
            using FileStream fs = new(_saveFileName, FileMode.OpenOrCreate);
            fs.SetLength(0);
            new XmlSerializer(typeof(SettingsType)).Serialize(fs, _settings);
        }
    }
}
