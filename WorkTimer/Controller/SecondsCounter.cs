using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Timers;

namespace WorkTimer.Controller;

// Timer ist immer dann präziser, wenn Start- und Stop-Signale nicht zu vollen 1000ms passieren (also in der Realität).
// Das alte Konzept war bei exaktem stoppen (<10 ms Abweichung) genauer.
// Bei exaktem stoppen nach 2000ms kann in diesem Timer-Ansatz noch 1 Sekunde eingetragen sein, da dann das Enabled=false kurz vor dem Invoken des Events passiert.
// Der Timer ist jedoch näher an der Realität: Wird z.B. nach 2212 ms gestoppt, steht 2 Sekunden auf der Uhr.
// Im alten Konzept wurde die Schleife noch durchlaufen bis zu Sekunde 3.

public partial class SecondsCounter : ObservableObject
{
    public bool IsRunning => _timer.Enabled;
    public TimeSpan SecondsAsTimeSpan => new(0, 0, Seconds);
    public string SecondsAsTimeString => SecondsAsTimeSpan.ToString("c");

    private System.Timers.Timer _timer;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SecondsAsTimeSpan))]
    [NotifyPropertyChangedFor(nameof(SecondsAsTimeString))]
    private int _seconds;

    public SecondsCounter()
    {
        _timer = new System.Timers.Timer();
        _timer.Enabled = false;
        _timer.Interval = 1000;
        _timer.Elapsed += _timer_Elapsed;
    }

    public void Run()
    {
        _timer.Start();
    }

    public void Pause()
    {
        _timer.Stop();
    }

    public void Reset()
    {
        Pause();
        Seconds = 0;
    }

    private void _timer_Elapsed(object? sender, ElapsedEventArgs e)
    {
        Seconds++;
    }
}