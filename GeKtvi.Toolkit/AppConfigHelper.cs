using System;
using System.IO;
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
    }
}
