using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Xml.Serialization;

namespace GeKtvi.Toolkit
{
    public class SettingsManager<SettingsType> : ISettingsManager<SettingsType>
    {
        public event EventHandler<SettingsType>? AfterLoad;

        protected string SaveFileName { get; init; }
        protected string SaveDirectory { get; init; }
        protected SettingsType? Settings { get; set; }

        private bool _isDisposed = false;
        private readonly Func<SettingsType> _settingsFactory;

        public SettingsManager(
            string folder,
            Func<SettingsType> settingsFactory,
            string? folderDirectory = null,
            string? fileName = null,
            Action<SettingsType>? afterLoad = null)
        {
            folderDirectory ??= Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            fileName ??= typeof(SettingsType).Name + ".save";

            SaveDirectory = $@"{folderDirectory}\{folder}";
            SaveFileName = $@"{SaveDirectory}\{fileName}";

            _settingsFactory = settingsFactory;

            if (afterLoad is not null)
                AfterLoad += (s, e) => afterLoad(e);
        }

        public SettingsType? TryLoad()
        {
            try
            {
                LoadFile();
            }
            catch (IOException)
            {
                Settings = _settingsFactory.Invoke();
            }
            catch (InvalidOperationException)
            {
                Settings = _settingsFactory.Invoke();
            }
            AfterLoad?.Invoke(this, Settings);
            return Settings;
        }

        public SettingsType Load()
        {
            LoadFile();
            AfterLoad?.Invoke(this, Settings);
            return Settings;
        }

#if NET6_0_OR_GREATER
        [MemberNotNull(nameof(Settings))]
#endif
        protected virtual void LoadFile()
        {
            using FileStream fileStream = new(SaveFileName, FileMode.OpenOrCreate);
            using StreamReader fs = new(fileStream);

            Settings = (SettingsType)(new XmlSerializer(typeof(SettingsType)).Deserialize(fs)
                ?? throw new InvalidDataException($"Cant cast serialized object to {nameof(SettingsType)}"));
        }

        public virtual void Save()
        {
            if (Settings is null)
                return;

            Directory.CreateDirectory(SaveDirectory);
            using FileStream fileStream = new(SaveFileName, FileMode.OpenOrCreate);
            fileStream.SetLength(0);
            using StreamWriter sw = new(fileStream);
            new XmlSerializer(typeof(SettingsType)).Serialize(sw, Settings);
        }

        public virtual void Dispose()
        {
            if(_isDisposed == false)
            {
                GC.SuppressFinalize(this);
                Save();
                _isDisposed = true;
            }
        }
    }
}
