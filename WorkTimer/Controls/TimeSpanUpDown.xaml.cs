using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WorkTimer.Controls
{
    public partial class TimeSpanUpDown : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty TimeSpanValueProperty =
        DependencyProperty.Register("TimeSpanValue", typeof(TimeSpan), typeof(TimeSpanUpDown),
            new FrameworkPropertyMetadata(TimeSpan.Zero, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public TimeSpan TimeSpanValue
        {
            get
            {
                return (TimeSpan)GetValue(TimeSpanValueProperty);
            }
            set
            {
                SetValue(TimeSpanValueProperty, value);
                OnTimeSpanValueChanged();
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty MaxTimeSpanValueProperty =
        DependencyProperty.Register("MaxTimeSpanValue", typeof(TimeSpan), typeof(TimeSpanUpDown),
            new FrameworkPropertyMetadata(new TimeSpan(23, 59, 59), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public TimeSpan MaxTimeSpanValue
        {
            get
            {
                return (TimeSpan)GetValue(MaxTimeSpanValueProperty);
            }
            set
            {
                SetValue(MaxTimeSpanValueProperty, value);
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? TimeSpanValueChanged;

        private void OnTimeSpanValueChanged()
        {
            TimeSpanValueChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TimeSpanValue)));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TimeSpanUpDown()
        {
            InitializeComponent();
        }

        private void ButtonUp_Click(object sender, RoutedEventArgs e)
        {
            TryManipulateTime(TimeCangeDirection.Up);
        }

        private void ButtonDown_Click(object sender, RoutedEventArgs e)
        {
            TryManipulateTime(TimeCangeDirection.Down);
        }

        private enum TimeCangeDirection
        {
            Up,
            Down
        }

        private void TryManipulateTime(TimeCangeDirection direction)
        {
            TimeSpan addingSpan = GetChangeSpan(direction);
            var TimeSpanValueDummy = new TimeSpan(TimeSpanValue.Ticks).Add(addingSpan);
            if (TimeSpanValueDummy < TimeSpan.Zero)
            {
                TimeSpanValue = TimeSpan.Zero;
                return;
            }
            if (TimeSpanValueDummy > MaxTimeSpanValue)
            {
                TimeSpanValue = MaxTimeSpanValue;
                return;
            }
            TimeSpanValue = TimeSpanValueDummy;
        }

        private static TimeSpan GetChangeSpan(TimeCangeDirection direction)
        {
            int changeValue = 1;
            if (direction == TimeCangeDirection.Down) changeValue *= -1;
            TimeSpan addingSpan = new TimeSpan(0, 0, changeValue);
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                addingSpan = new TimeSpan(0, changeValue, 0);
            }
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                addingSpan = new TimeSpan(changeValue, 0, 0);
            }
            return addingSpan;
        }

        private void UserControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                TryManipulateTime(TimeCangeDirection.Up);
            }
            else
            {
                TryManipulateTime(TimeCangeDirection.Down);
            }
        }

        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox)?.SelectionLength > 0)
            {
                (sender as TextBox).SelectionLength = 0;
            }
            e.Handled = true;
        }
    }
}