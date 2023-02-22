using System;
using System.Collections.Generic;
using WorkTimer.Model;
using WorkTimer.Services;

namespace WorkTimer.Controller.SpecialDataControllers
{
    public class TimerDataController : IDataController
    {
        public void LoadData()
        {
            Data.TodayJobTimer = GetTimerData(DateTime.Today);
        }

        public bool SaveData()
        {
            return SaveTimerData(Data.TodayJobTimer);
        }

        private static string DirectoryPath => Data.Settings.DataSavePath;
        private static readonly string SaveFileNameDateTemplate = "JobTimerData_{year}_{month}_{day}";
        private static readonly string SaveFileNameExtension = ".jtd";
        private static string SaveFileNameTemplate => $"{SaveFileNameDateTemplate}{SaveFileNameExtension}";

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

        public static JobTimerModel GetTimerData(DateTime day)
        {
            var thisDayData = FileService.LoadFile<JobTimerModel>(GetFilePath(day));
            if (thisDayData != null && thisDayData.Date.Date == day.Date)
            {
                return thisDayData;
            }
            return new JobTimerModel();
        }

        public static IEnumerable<JobTimerModel> LoadHistoryData(DateTime fromDate, DateTime? toDate = null, bool exceptToday = true)
        {
            DateTime currentDate = new DateTime(fromDate.Ticks);
            if (toDate == null) toDate = DateTime.Today;
            while (currentDate <= toDate)
            {
                if (!exceptToday || currentDate.Date != DateTime.Today)
                {
                    var historyData = FileService.LoadFile<JobTimerModel>(GetFilePath(currentDate));
                    if (historyData != null)
                    {
                        yield return historyData;
                    }
                }
                currentDate = currentDate.AddDays(1);
            }
        }

        public static bool SaveTimerData(JobTimerModel data)
        {
            return FileService.SaveFile(GetFilePath(data.Date), data);
        }
    }
}