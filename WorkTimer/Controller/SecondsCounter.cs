using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WorkTimer.Controller;

public partial class SecondsCounter : ObservableObject
{
    private readonly TimeSpan _interval;

    private bool _isRunning;
    public bool IsRunning => _isRunning;

    private Task? _countTask;

    [ObservableProperty]
    private int _seconds;

    public SecondsCounter()
    {
        _interval = new TimeSpan(0, 0, 1);
        _isRunning = false;
    }

    //public TimeSpan SecondsAsTimeSpan => new(0, 0, Seconds);

    public void Run()
    {
        _isRunning = true;
        _countTask = new Task(StartCounting);
        _countTask.Start();
    }

    public void Pause()
    {
        _isRunning = false;
        if (_countTask != null)
        {
            _countTask.Wait();
            _countTask.Dispose();
        }
    }

    public void Reset()
    {
        Pause();
        Seconds = 0;
    }

    private void StartCounting()
    {
        var nextTick = DateTime.Now + _interval;
        while (_isRunning)
        {
            while (DateTime.Now < nextTick) Thread.Sleep(nextTick - DateTime.Now);
            nextTick += _interval;
            Seconds++;
        }
    }
}