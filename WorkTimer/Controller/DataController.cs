using System.Collections.Generic;
using WorkTimer.Controller.SpecialDataControllers;
using WorkTimer.Model;

namespace WorkTimer.Controller

{
    public static class DataControllers
    {
        public static SettingsDataController SettingsDataController { get; private set; } = new();
        public static TimerDataController TimerDataController { get; private set; } = new();

        private static readonly List<IDataController> SpecialDataControllers = new()
        {
            SettingsDataController,
            TimerDataController
        };

        public static void LoadAllData()
        {
            foreach (var dataController in SpecialDataControllers)
            {
                dataController.LoadData();
            }
        }

        public static void SaveAllData()
        {
            foreach (var dataController in SpecialDataControllers)
            {
                dataController.SaveData();
            }
        }

        public static void ReloadAllData()
        {
            SaveAllData();
            Data.ClearData();
            LoadAllData();
        }
    }
}