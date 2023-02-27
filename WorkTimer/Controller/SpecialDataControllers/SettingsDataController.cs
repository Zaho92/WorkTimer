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

        private SettingsModel GetSettings()
        {
            var thisSettings = FileService.LoadFile<SettingsModel>(CompleteFilePath);
            if (thisSettings != null)
            {
                return thisSettings;
            }
            return new SettingsModel();
        }

        private bool SaveSettings(SettingsModel settings)
        {
            return FileService.SaveFile(CompleteFilePath, settings);
        }

        public void HandleDataPathChange(string newValue)
        {
            if (Data.Settings == null) return;
            if (String.IsNullOrWhiteSpace(Data.Settings.DataSavePath))
            {
                Data.Settings.DataSavePath = Data.Settings.LasValidDataSavePath;
            }
            else
            {
                if (Data.Settings.DataSavePath != Data.Settings.LasValidDataSavePath)
                {
                    string header = "Daten Speicherort geändert";
                    string message = $"Der Speicherort der Daten wurde von\n{Data.Settings.LasValidDataSavePath}\nzu\n{Data.Settings.DataSavePath}\ngeändert. Möchten Sie auch alle darin enthaltenen Daten mit umziehen?\nWenn nicht verbleiben diese am vorherigen Ort aber werden vom Programm nicht mehr abgerufen.";
                    MessageController.ShowMessageWindow(MessageController.MessageType.YesNo, header, message);
                }
                Data.Settings.LasValidDataSavePath = Data.Settings.DataSavePath;
            }
        }
    }
}