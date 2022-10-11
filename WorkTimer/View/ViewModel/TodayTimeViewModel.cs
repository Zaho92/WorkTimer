using System;
using System.Windows.Input;
using System.Windows.Threading;
using WorkTimer.Model;

namespace WorkTimer.View.ViewModel;

internal class TodayTimeViewModel : ObservableObject
{
    private readonly DispatcherTimer _timer = new() { Interval = TimeSpan.FromSeconds(1) };
    private bool _canStartBreakStopwatchCommand;
    private bool _canStartWorkStopwatchCommand;
    private TodayTimeModel? _currentModel;
    private ICommand? _startBreakStopwatchCommand;

    private DateTime _startDateTime;
    private ICommand? _startWorkStopwatchCommand;

    public TodayTimeViewModel()
    {
        CanStartWorkStopwatchCommand = true;
        CanStartBreakStopwatchCommand = true;
        CurrentModel = new TodayTimeModel();
        StartWorkStopwatch();
    }

    public TodayTimeModel? CurrentModel
    {
        get => _currentModel;
        set
        {
            if (value != _currentModel)
            {
                _currentModel = value;
                OnPropertyChanged("CurrentModel");
            }
        }
    }

    public bool CanStartWorkStopwatchCommand
    {
        get => _canStartWorkStopwatchCommand;
        set
        {
            if (value != _canStartWorkStopwatchCommand)
            {
                _canStartWorkStopwatchCommand = value;
                OnPropertyChanged("CanStartWorkStopwatchCommand");
            }
        }
    }

    public bool CanStartBreakStopwatchCommand
    {
        get => _canStartBreakStopwatchCommand;
        set
        {
            if (value != _canStartBreakStopwatchCommand)
            {
                _canStartBreakStopwatchCommand = value;
                OnPropertyChanged("CanStartBreakStopwatchCommand");
            }
        }
    }

    public ICommand? StartWorkStopwatchCommand
    {
        get
        {
            if (_startWorkStopwatchCommand == null)
                _startWorkStopwatchCommand = new RelayCommand(
                    param => StartWorkStopwatch(),
                    param => CurrentModel != null
                );
            return _startWorkStopwatchCommand;
        }
    }


    public ICommand? StartBreakStopwatchCommand
    {
        get
        {
            if (_startBreakStopwatchCommand == null)
                _startBreakStopwatchCommand = new RelayCommand(
                    param => StartBreakStopwatch(),
                    param => CurrentModel != null
                );
            return _startBreakStopwatchCommand;
        }
    }

    private void StartWorkStopwatch()
    {
        _timer.Stop();
        CanStartWorkStopwatchCommand = false;
        CanStartBreakStopwatchCommand = true;
        _timer.Tick -= BreakTimer_Tick;
        _timer.Tick += WorkTimer_Tick;
        _startDateTime = DateTime.Now;
        _timer.Start();
    }

    private void StartBreakStopwatch()
    {
        _timer.Stop();
        CanStartWorkStopwatchCommand = true;
        CanStartBreakStopwatchCommand = false;
        _timer.Tick -= WorkTimer_Tick;
        _timer.Tick += BreakTimer_Tick;
        _startDateTime = DateTime.Now;
        _timer.Start();
    }

    private void WorkTimer_Tick(object? sender, EventArgs e)
    {
        CurrentModel.TodayWorkTime += DateTime.Now - _startDateTime;
        _startDateTime = DateTime.Now;
    }

    private void BreakTimer_Tick(object? sender, EventArgs e)
    {
        CurrentModel.TodayBreakTime += DateTime.Now - _startDateTime;
        _startDateTime = DateTime.Now;
    }
}