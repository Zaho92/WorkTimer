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
    private int _seconds;

    [JsonIgnore]
    public TimeSpan SecondsAsTimeSpan => new(0, 0, Seconds);

    [JsonIgnore]
    public string SecondsAsTimeString => SecondsAsTimeSpan.ToString("c");

    [JsonIgnore]
    public double SecondsAsHours => new TimeSpan(0, 0, Seconds).TotalHours;
}