using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;


namespace GeKtvi.Toolkit
{
    public enum SerializerType
    {
        Json,
        Xml
    }

    public class AppConfigHelper
    {
        public static T LoadConfig<T>(string fileName = "AppConfig.config", string folder = "Configs") =>
            LoadConfig<T>(SerializerType.Xml, fileName, folder);

        public static T LoadConfig<T>(SerializerType serializer, string fileName = "AppConfig.config", string folder = "Configs")
        {
            string fullPath = Path.Combine(folder, fileName);
            if (File.Exists(fullPath) == false)
                throw new FileNotFoundException("Config not found", fullPath);

            using FileStream fileStream = new(fullPath, FileMode.Open, FileAccess.Read);
            using StreamReader sr = new(fileStream);

            object? res;

            switch (serializer)
            {
                case SerializerType.Xml:
                    {
                        XmlSerializer ser = new(typeof(T));
                        res = ser.Deserialize(sr);
                        break;
                    }
                case SerializerType.Json:
                    {
                        DataContractJsonSerializer ser = new (typeof(T));

                        res = ser.ReadObject(fileStream);
                    }
                    break;
                default:
                    throw UnsupportedSerializer(serializer);
            }

            if (res is not T config)
                throw SerializedObjectCastException<T>();

            return config;
        }

        public static void WriteConfig(object config, string fileName = "AppConfig.config", string folder = "Configs") =>
            WriteConfig(config, SerializerType.Xml, fileName, folder);

        public static void WriteConfig(object config, SerializerType serializer, string fileName = "AppConfig.config", string folder = "Configs")
        {
            string fullPath = Path.Combine(folder, fileName);

            Directory.CreateDirectory(folder);

            using var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write);
            fileStream.SetLength(0);
            using StreamWriter sr = new(fileStream);

            switch (serializer)
            {
                case SerializerType.Xml:
                    {
                        XmlSerializer ser = new(config.GetType());
                        ser.Serialize(sr, config);
                        break;
                    }
                case SerializerType.Json:
                    {

                        DataContractJsonSerializer ser = new (config.GetType());
                        using MemoryStream ms = new();
                        ser.WriteObject(ms, config);
                        ms.Position = 0;
                        sr.Write(new StreamReader(ms).ReadToEnd());
                    }
                    break;
                default:
                    throw new ArgumentException("Unsupported serializer", nameof(serializer));
            }
        }

        public static IEnumerable<T> LoadArrayConfigs<T>(string folder = "Configs", string filePattern = "*.config")
        {
            var files = Directory.GetFiles(folder, filePattern);
            if (files.Any() == false)
                throw new InvalidDataException("Folder does not contains matching elements");

            List<T> list = [];
            foreach (var file in files)
                list.AddRange(LoadConfig<T[]>(file, string.Empty));
            return list;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ArgumentException UnsupportedSerializer(SerializerType serializer) =>
            new($"Unsupported serializer: {serializer}");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static InvalidDataException SerializedObjectCastException<T>() =>
            new($"Cant cast serialized object to {nameof(T)}");

    }
}
