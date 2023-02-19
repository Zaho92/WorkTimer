using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WorkTimer.Controller;
using WorkTimer.Helpers;
using WorkTimer.Model;

namespace WorkTimer.ViewModel
{
    public partial class StatisticsViewModel : ObservableObject
    {
        [ObservableProperty]
        private SecondsModel _thisWeekWorkTime;

        [ObservableProperty]
        private SecondsModel _thisWeekBreakTime;

        [ObservableProperty]
        private SecondsModel _thisMonthWorkTime;

        [ObservableProperty]
        private SecondsModel _thisMonthBreakTime;

        private int thisWeekWorkSecondsWithoutToday;
        private int thisWeekBreakSecondsWithoutToday;
        private int thisMonthWorkSecondsWithoutToday;
        private int thisMonthBreakSecondsWithoutToday;

        [ObservableProperty]
        private string _chartTitle;

        [ObservableProperty]
        public Dictionary<string, JobTimerModel> _chartData;

        public bool CanShowPreviousWeekChart => CurrenChartRefenrenceDate.Year > 1 || CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(CurrenChartRefenrenceDate, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) > 1;
        public bool CanShowNextWeekChart => CurrenChartRefenrenceDate < CultureInfo.CurrentCulture.Calendar.FirstDateOfWeekContainingDate(DateTime.Today);

        private DateTime _currenChartRefenrenceDate;

        private DateTime CurrenChartRefenrenceDate
        {
            get
            {
                return _currenChartRefenrenceDate;
            }
            set
            {
                _currenChartRefenrenceDate = value;
                UpdateChart();
                OnPropertyChanged(nameof(CanShowPreviousWeekChart));
                OnPropertyChanged(nameof(CanShowNextWeekChart));
            }
        }

        public StatisticsViewModel()
        {
            ThisWeekWorkTime = new SecondsModel();
            ThisWeekBreakTime = new SecondsModel();
            ThisMonthWorkTime = new SecondsModel();
            ThisMonthBreakTime = new SecondsModel();
            CurrenChartRefenrenceDate = DateTime.Today;
            LoadThisWeekStataistics();
            LoadThisMonthStatistics();
            Data.TodayJobTimer.WorkTime.PropertyChanged += WorkTime_PropertyChanged;
            Data.TodayJobTimer.BreakTime.PropertyChanged += BreakTime_PropertyChanged;
        }

        private void WorkTime_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ThisWeekWorkTime.Seconds = thisWeekWorkSecondsWithoutToday + Data.TodayJobTimer.WorkTime.Seconds;
            ThisMonthWorkTime.Seconds = thisMonthWorkSecondsWithoutToday + Data.TodayJobTimer.WorkTime.Seconds;
        }

        private void BreakTime_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ThisWeekBreakTime.Seconds = thisWeekBreakSecondsWithoutToday + Data.TodayJobTimer.BreakTime.Seconds;
            ThisMonthBreakTime.Seconds = thisMonthBreakSecondsWithoutToday + Data.TodayJobTimer.BreakTime.Seconds;
        }

        private void LoadThisWeekStataistics()
        {
            DateTime firstDayOfWeek = CultureInfo.CurrentCulture.Calendar.FirstDateOfWeekContainingDate(DateTime.Today);
            DateTime lastDayOfWeek = firstDayOfWeek.AddDays(6);
            var thisWeekData = DataController.LoadHistoryData(firstDayOfWeek, lastDayOfWeek);
            ThisWeekWorkTime.Seconds = thisWeekWorkSecondsWithoutToday = thisWeekData.Sum(jtm => jtm?.WorkTime?.Seconds ?? 0);
            ThisWeekBreakTime.Seconds = thisWeekBreakSecondsWithoutToday = thisWeekData.Sum(jtm => jtm?.BreakTime?.Seconds ?? 0);
        }

        private void LoadThisMonthStatistics()
        {
            DateTime today = DateTime.Today;
            DateTime firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var thisWeekData = DataController.LoadHistoryData(firstDayOfMonth, lastDayOfMonth);
            ThisMonthWorkTime.Seconds = thisMonthWorkSecondsWithoutToday = thisWeekData.Sum(jtm => jtm?.WorkTime?.Seconds ?? 0);
            ThisMonthBreakTime.Seconds = thisMonthBreakSecondsWithoutToday = thisWeekData.Sum(jtm => jtm?.BreakTime?.Seconds ?? 0);
        }

        [RelayCommand]
        public void ShowPreviousChartWeek()
        {
            if (CanShowPreviousWeekChart)
            {
                CurrenChartRefenrenceDate = CurrenChartRefenrenceDate.AddDays(-7);
            }
        }

        [RelayCommand]
        public void ShowNextChartWeek()
        {
            if (CanShowNextWeekChart)
            {
                CurrenChartRefenrenceDate = CurrenChartRefenrenceDate.AddDays(7);
            }
        }

        private void UpdateChart()
        {
            DateTime firstDayOfWeek = CultureInfo.CurrentCulture.Calendar.FirstDateOfWeekContainingDate(CurrenChartRefenrenceDate);
            DateTime lastDayOfWeek = firstDayOfWeek.AddDays(6);
            GererateChartData(firstDayOfWeek, lastDayOfWeek);
        }

        private void GererateChartData(DateTime fromDate, DateTime toDate)
        {
            ChartTitle = $"{fromDate.ToLongDateString()} bis {toDate.ToLongDateString()}";
            var DataDictionary = new Dictionary<string, JobTimerModel>();
            var currentWeekData = DataController.LoadHistoryData(fromDate, toDate);
            while (fromDate <= toDate)
            {
                DataDictionary.Add(fromDate.ToShortDateString(), currentWeekData.FirstOrDefault(jtm => jtm.Date.Date == fromDate.Date) ?? new JobTimerModel());
                fromDate = fromDate.AddDays(1);
            }
            ChartData = DataDictionary;
        }
    }
}