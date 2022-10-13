using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WorkTimer.Controller;

public partial class SecondsCounter : ObservableObject
{
    private readonly TimeSpan _interval;

    private bool _isRunning;
    private Task countTask;

    [ObservableProperty]
    //[NotifyPropertyChangedFor(nameof(SecondsAsTimeSpan))]
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
        countTask = new Task(StartCounting);
        countTask.Start();
    }

    public void Pause()
    {
        _isRunning = false;
        if (countTask != null)
        {
            countTask.Wait();
            countTask.Dispose();
        }
    }

    public void Reset()
    {
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