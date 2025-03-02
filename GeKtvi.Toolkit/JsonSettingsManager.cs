using System;
using System.IO;
using System.Runtime.Serialization.Json;


namespace GeKtvi.Toolkit
{
    public class JsonSettingsManager<SettingsT>(string folder,
                                                  Func<SettingsT> settingsFactory,
                                                  string? folderDirectory = null,
                                                  string? fileName = null,
                                                  Action<SettingsT>? afterLoad = null)
        : SettingsManager<SettingsT>(folder, settingsFactory, folderDirectory, fileName, afterLoad)
    {
        protected override void LoadFile()
        {
            using FileStream fileStream = new(SaveFileName, FileMode.OpenOrCreate);

            DataContractJsonSerializer ser = new(typeof(SettingsT));

            Settings = (SettingsT)(ser.ReadObject(fileStream) 
                ?? throw new InvalidDataException($"Cant cast serialized object to {nameof(SettingsT)}"));
        }

        public override void Save()
        {
            if (Settings is null)
                return;

            Directory.CreateDirectory(SaveDirectory);

            using var fileStream = new FileStream(SaveFileName, FileMode.OpenOrCreate, FileAccess.Write);
            fileStream.SetLength(0);

            DataContractJsonSerializer ser = new(Settings.GetType());
            ser.WriteObject(fileStream, Settings);
        }
    }
}
