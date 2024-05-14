using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Xml.Serialization;

namespace GeKtvi.Toolkit
{
    public class SettingsManager<SettingsType> : IDisposable
    {
        public event EventHandler<SettingsType>? AfterLoad;

        private readonly string _saveFileName;
        private readonly string _saveDirectory;
        private readonly Func<SettingsType> _settingsFactory;
        private readonly Func<SettingsType, bool> _savingRequiredSelector;
        private SettingsType? _settings;

        public SettingsManager(
            string folder,
            Func<SettingsType> settingsFactory,
            Func<SettingsType, bool>? savingRequiredSelector = null,
            string? folderDirectory = null,
            string? fileName = null,
            Action<SettingsType>? afterLoad = null)
        {
            
            _savingRequiredSelector = savingRequiredSelector ?? (s => true);
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
            catch (InvalidDataException)
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
            if (_settings is null)
                return;
            if(!_savingRequiredSelector.Invoke(_settings))
                return;
            Directory.CreateDirectory(_saveDirectory);
            using FileStream fs = new(_saveFileName, FileMode.OpenOrCreate);
            fs.SetLength(0);
            new XmlSerializer(typeof(SettingsType)).Serialize(fs, _settings);
        }

        public bool TrySave()
        {
            try
            {
                Save();
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }

        /// <summary>
        /// Tries save settings. Uses for auto save with using in DI containers
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            TrySave();
        }
    }
}
