using System;
using System.IO;
using System.Text.Json;
using WorkTimer.Model;

namespace WorkTimer.Controller
{
    public static class DataController
    {
        private static string DataPath => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\JobTimer\Data\";

        private const string FileNameDateTemplate = "JobTimerData_{year}_{month}_{day}";
        private const string FileNameExtension = ".jtd";
        private static string FileNameTemplate => $"{FileNameDateTemplate}{FileNameExtension}";

        public static void LoadData()
        {
            if (!Directory.Exists(DataPath)) return;
            foreach (string filename in Directory.EnumerateFiles(DataPath))
            {
                if (!filename.EndsWith(FileNameExtension)) continue;
                try
                {
                    string jsonString = File.ReadAllText(filename);
                    if (JsonSerializer.Deserialize(jsonString, typeof(JobTimerModel)) is JobTimerModel model)
                    {
                        if (model.Date.Equals(DateTime.Today))
                        {
                            Data.TodayJobTimer = model;
                        }
                        else
                        {
                            Data.PH_HistoryTimerData.Add(DateOnly.FromDateTime(model.Date), model);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw; // TODO EXCEPTION-MANAGEMENT
                }
            }
        }

        public static void SaveData()
        {
            //Daten speichern
            if (!Directory.Exists(DataPath))
            {
                Directory.CreateDirectory(DataPath);
            }
            try
            {
                string filename = GetFileName(Data.TodayJobTimer.Date);
                string jsonString = JsonSerializer.Serialize(Data.TodayJobTimer);
                File.WriteAllText(DataPath + filename, jsonString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw; // TODO EXCEPTION-MANAGEMENT
            }
        }

        private static string GetFileName(DateTime date)
        {
            return FileNameTemplate
                .Replace("{year}", date.Year.ToString())
                .Replace("{month}", date.Month.ToString())
                .Replace("{day}", date.Day.ToString());
        }
    }
}