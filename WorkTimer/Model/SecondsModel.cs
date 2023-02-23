using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Text.Json.Serialization;
using System.Timers;

namespace WorkTimer.Model;

public partial class SecondsModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SecondsAsTimeSpan))]
    [NotifyPropertyChangedFor(nameof(SecondsAsTimeString))]
    [NotifyPropertyChangedFor(nameof(SecondsAsHours))]
    private int _seconds;

    [JsonIgnore]
    public TimeSpan SecondsAsTimeSpan => new(0, 0, Seconds);

    [JsonIgnore]
    public string SecondsAsTimeString => FormatTimeFromSeconds();// string.Format("{0:hh\\:mm\\:ss}", SecondsAsTimeSpan);

    [JsonIgnore]
    public double SecondsAsHours => SecondsAsTimeSpan.TotalHours;

    public string FormatTimeFromSeconds()
    {
        int hours = Seconds / 3600;
        int minutes = (Seconds % 3600) / 60;
        int seconds = Seconds % 60;
        return string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
    }
}