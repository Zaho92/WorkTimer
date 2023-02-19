using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using WorkTimer.Model;

namespace WorkTimer.Controller
{
    public static class DataController
    {
        private static string DirectoryPath => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\JobTimer\Data\";

        private static string SaveFileNameTemplate => $"{SaveFileNameDateTemplate}{SaveFileNameExtension}";

        private const string SaveFileNameDateTemplate = "JobTimerData_{year}_{month}_{day}";

        private const string SaveFileNameExtension = ".jtd";

        private static string GetFilePath(DateTime date)
        {
            return DirectoryPath + GetFileName(date);
        }

        private static string GetFileName(DateTime date)
        {
            return SaveFileNameTemplate
                   .Replace("{year}", date.Year.ToString())
                   .Replace("{month}", date.Month.ToString())
                   .Replace("{day}", date.Day.ToString());
        }

        public static void ReloadData()
        {
            SaveTodayData();
            Data.ClearData();
            LoadTodayData();
        }

        public static void LoadTodayData()
        {
            var todayData = LoadFile<JobTimerModel>(GetFilePath(DateTime.Today));
            if (todayData != null)
            {
                Data.TodayJobTimer = todayData;
            }
        }

        public static IEnumerable<JobTimerModel> LoadHistoryData(DateTime fromDate, DateTime? toDate = null, bool exceptToday = true)
        {
            DateTime currentDate = new DateTime(fromDate.Ticks);
            while (currentDate <= toDate)
            {
                if (!exceptToday || currentDate.Date != DateTime.Today)
                {
                    var historyData = LoadFile<JobTimerModel>(GetFilePath(currentDate));
                    if (historyData != null)
                    {
                        yield return historyData;
                    }
                }
                currentDate = currentDate.AddDays(1);
            }
        }

        private static T? LoadFile<T>(string path)
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

        public static bool SaveTodayData()
        {
            return SaveFile(GetFilePath(Data.TodayJobTimer.Date), Data.TodayJobTimer);
        }

        private static bool SaveFile<T>(string path, T data)
        {
            if (!File.Exists(path)) return false;
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