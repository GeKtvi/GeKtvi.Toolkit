using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace GeKtviWpfToolkit
{
    public class AppConfigHelper
    {
        public static T LoadConfig<T>(string fileName = "AppConfig.config", string folder = "Properties\\Configs")
        {
            T config;
            string fullPath = Path.Combine(folder, fileName);
            if (File.Exists(fullPath) == false)
                throw new FileNotFoundException("Config not found", fullPath);
            
            XmlSerializer xml = new(typeof(T));

            using FileStream fs = new(fullPath, FileMode.Open, FileAccess.Read);
            config = (T)xml.Deserialize(fs);

            return config;
        }

        public static void WriteConfig(object config,string fileName = "AppConfig.config", string folder = "Properties\\Configs")
        {
            string fullPath = Path.Combine(folder, fileName);

            Type objType = config.GetType();

            XmlSerializer xml = new(objType);

            using FileStream fs = new(fullPath, FileMode.OpenOrCreate, FileAccess.Write);
            xml.Serialize(fs, config);
        }
    }
}
