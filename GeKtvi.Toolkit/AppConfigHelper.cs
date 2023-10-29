using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace GeKtvi.Toolkit
{
    public class AppConfigHelper
    {
        public static T LoadConfig<T>(string fileName = "AppConfig.config", string folder = "Configs")
        {
            T config;
            string fullPath = Path.Combine(folder, fileName);
            if (File.Exists(fullPath) == false)
                throw new FileNotFoundException("Config not found", fullPath);

            XmlSerializer xml = new(typeof(T));

            using FileStream fs = new(fullPath, FileMode.Open, FileAccess.Read);
            config = (T)(xml.Deserialize(fs) ?? throw new InvalidDataException($"Cant cast serialized object to {nameof(T)}"));

            return config;
        }

        public static void WriteConfig(object config, string fileName = "AppConfig.config", string folder = "Configs")
        {
            string fullPath = Path.Combine(folder, fileName);

            Type objType = config.GetType();

            XmlSerializer xml = new(objType);

            using FileStream fs = new(fullPath, FileMode.OpenOrCreate, FileAccess.Write);
            xml.Serialize(fs, config);
        }

        public static IEnumerable<T> LoadArrayConfigs<T>(string folder = "Configs", string filePattern = "*.config")
        {
            var files = Directory.GetFiles(folder, filePattern);
            if (files.Any() == false)
                throw new InvalidDataException("Folder does not contains matching elements");

            List<T> list = new();
            foreach (var file in files)
                list.AddRange(LoadConfig<T[]>(file, string.Empty));
            return list;
        }
    }
}
