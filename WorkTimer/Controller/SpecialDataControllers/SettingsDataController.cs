using System;
using WorkTimer.Model;
using WorkTimer.Services;

namespace WorkTimer.Controller.SpecialDataControllers
{
    public class SettingsDataController : IDataController
    {
        public void LoadData()
        {
            Data.Settings = GetSettings();
        }

        public bool SaveData()
        {
            return SaveSettings(Data.Settings);
        }

        private static string DirectoryPath => Environment.CurrentDirectory + @"\AppData\Settings\";
        private static readonly string SaveFileName = "JobTimerSettings";
        private static readonly string SaveFileNameExtension = ".config";
        private static string SaveFileNameTemplate => $"{SaveFileName}{SaveFileNameExtension}";
        private static string CompleteFilePath => $"{DirectoryPath}{SaveFileNameTemplate}";

        private static SettingsModel GetSettings()
        {
            var thisSettings = FileService.LoadFile<SettingsModel>(CompleteFilePath);
            if (thisSettings != null)
            {
                return thisSettings;
            }
            return new SettingsModel();
        }

        private static bool SaveSettings(SettingsModel settings)
        {
            return FileService.SaveFile(CompleteFilePath, settings);
        }
    }
}