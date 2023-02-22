using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WorkTimer.Services
{
    public static class FileService
    {
        public static T? LoadFile<T>(string path)
        {
            if (!File.Exists(path)) return default;
            try
            {
                string jsonString = File.ReadAllText(path);
                if (JsonSerializer.Deserialize(jsonString, typeof(T)) is T model)
                {
                    return model;
                }
                return default;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw; // TODO EXCEPTION-MANAGEMENT
            }
        }

        public static bool SaveFile<T>(string path, T data)
        {
            string? directoryPath = Path.GetDirectoryName(path);
            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
            try
            {
                string jsonString = JsonSerializer.Serialize(data);
                File.WriteAllText(path, jsonString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw; // TODO EXCEPTION-MANAGEMENT
            }
            return true;
        }
    }
}