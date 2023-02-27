using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;

namespace WorkTimer.Controls
{
    public partial class ValueUpDown : UserControl, INotifyPropertyChanged
    {
        private static readonly Type[] allowedTypes = new Type[] { typeof(int), typeof(double), typeof(decimal), typeof(float), typeof(TimeSpan) };
        private Type currentValueType;

        #region DP Value

        public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register("Value", typeof(object), typeof(ValueUpDown),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var valueUpDown = (ValueUpDown)d;
            valueUpDown.CheckAndSetType();
            valueUpDown.ValueChanged?.Invoke(valueUpDown, new PropertyChangedEventArgs(nameof(Value)));
            valueUpDown.OnPropertyChanged(nameof(Value));
        }

        public object Value
        {
            get
            {
                return GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }

        #endregion DP Value

        #region DP MinValue

        public static readonly DependencyProperty MinValueProperty =
        DependencyProperty.Register("MinValue", typeof(object), typeof(ValueUpDown),
            new FrameworkPropertyMetadata(null, OnMinValueChanged));

        private static void OnMinValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var valueUpDown = (ValueUpDown)d;
            valueUpDown.GetCorrectTypeDependingValue(valueUpDown.MinValue, nameof(MinValue));
            valueUpDown.OnPropertyChanged(nameof(MinValue));
        }

        public object MinValue
        {
            get
            {
                if (GetValue(MinValueProperty) == null || (GetValue(MinValueProperty) is string))
                {
                    SetValue(MinValueProperty, GetCorrectTypeDependingValue(GetValue(MinValueProperty), nameof(MinValue)));
                }
                return GetValue(MinValueProperty);
            }
            set
            {
                SetValue(MinValueProperty, value);
            }
        }

        #endregion DP MinValue

        #region DP MaxValue

        public static readonly DependencyProperty MaxValueProperty =
        DependencyProperty.Register("MaxValue", typeof(object), typeof(ValueUpDown),
            new FrameworkPropertyMetadata(null, OnMaxValueChanged));

        private static void OnMaxValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var valueUpDown = (ValueUpDown)d;
            valueUpDown.GetCorrectTypeDependingValue(valueUpDown.MaxValue, nameof(MaxValue));
            valueUpDown.OnPropertyChanged(nameof(MaxValue));
        }

        public object MaxValue
        {
            get
            {
                if (GetValue(MaxValueProperty) == null || (GetValue(MaxValueProperty) is string))
                {
                    SetValue(MaxValueProperty, GetCorrectTypeDependingValue(GetValue(MaxValueProperty), nameof(MaxValue)));
                }
                return GetValue(MaxValueProperty);
            }
            set
            {
                SetValue(MaxValueProperty, value);
            }
        }

        #endregion DP MaxValue

        #region DP ModifyPerClickValue

        public static readonly DependencyProperty ModifyPerClickValueProperty =
        DependencyProperty.Register("ModifyPerClickValue", typeof(object), typeof(ValueUpDown),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, OnModifyPerClickValuePropertyChanged));

        private static void OnModifyPerClickValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (ValueUpDown)d;
            control.OnPropertyChanged(nameof(ModifyPerClickValue));
        }

        public object ModifyPerClickValue
        {
            get
            {
                if (GetValue(ModifyPerClickValueProperty) == null || (GetValue(ModifyPerClickValueProperty) is string))
                {
                    SetValue(ModifyPerClickValueProperty, GetCorrectTypeDependingValue(GetValue(ModifyPerClickValueProperty), nameof(ModifyPerClickValue)));
                }
                return (object)GetValue(ModifyPerClickValueProperty);
            }
            set
            {
                SetValue(ModifyPerClickValueProperty, value);
            }
        }

        #endregion DP ModifyPerClickValue

        public string ShiftModifyValueText
        {
            get
            {
                if (currentValueType is null) return "";
                if (currentValueType == typeof(int))
                {
                    dynamic changeValue = (int)ModifyPerClickValue;
                    return $"den Wert um {changeValue * 10}";
                }
                else if (currentValueType == typeof(double))
                {
                    dynamic changeValue = (double)ModifyPerClickValue;
                    return $"den Wert um {changeValue * 10}";
                }
                else if (currentValueType == typeof(decimal))
                {
                    dynamic changeValue = (decimal)ModifyPerClickValue;
                    return $"den Wert um {changeValue * 10}";
                }
                else if (currentValueType == typeof(float))
                {
                    dynamic changeValue = (float)ModifyPerClickValue;
                    return $"den Wert um {changeValue * 10}";
                }
                else if (currentValueType == typeof(TimeSpan))
                {
                    return "die Minuten";
                }
                else
                {
                    throw new InvalidCastException($"Der Typ {currentValueType.Name} wird nicht unterstüzt.");
                }
            }
        }

        public string CtrlModifyValueText
        {
            get
            {
                if (currentValueType is null) return "";
                if (currentValueType == typeof(int))
                {
                    dynamic changeValue = (int)ModifyPerClickValue;
                    return $"den Wert um {changeValue * 100}";
                }
                else if (currentValueType == typeof(double))
                {
                    dynamic changeValue = (double)ModifyPerClickValue;
                    return $"den Wert um {changeValue * 100}";
                }
                else if (currentValueType == typeof(decimal))
                {
                    dynamic changeValue = (decimal)ModifyPerClickValue;
                    return $"den Wert um {changeValue * 100}";
                }
                else if (currentValueType == typeof(float))
                {
                    dynamic changeValue = (float)ModifyPerClickValue;
                    return $"den Wert um {changeValue * 100}";
                }
                else if (currentValueType == typeof(TimeSpan))
                {
                    return "die Stunden";
                }
                else
                {
                    throw new InvalidCastException($"Der Typ {currentValueType.Name} wird nicht unterstüzt.");
                }
            }
        }

        #region ValueTypeDependingChecks

        private void CheckAndSetType()
        {
            if (Value is not null)
            {
                currentValueType = Value.GetType();
                if (!allowedTypes.Contains(currentValueType))
                {
                    throw new InvalidCastException($"Der Type {currentValueType.Name} ist für Value nicht erlaubt.");
                }
            }
        }

        private object GetCorrectTypeDependingValue(object depValue, string propertyName)
        {
            if (currentValueType is null) return null;
            if (Value is null) return null;
            if (depValue is null)
            {
                return GetTypeSpecificStandardValue(propertyName);
            }
            if (depValue.GetType() != currentValueType)
            {
                if (depValue is string depStingValue)
                {
                    return TryConvertToAllowedType(depStingValue);
                }
                else
                {
                    throw new InvalidCastException($"Der Type {depValue.GetType().Name} ist für die von Value abhängige Eigenschaft zur Zeit nicht erlaubt.");
                }
            }
            return depValue;
        }

        private object GetTypeSpecificStandardValue(string propertyName)
        {
            if (currentValueType == typeof(int))
            {
                switch (propertyName)
                {
                    case "MinValue":
                        return 0;

                    case "MaxValue":
                        return int.MaxValue;

                    case "ModifyPerClickValue":
                        return 1;
                }
            }
            else if (currentValueType == typeof(double))
            {
                switch (propertyName)
                {
                    case "MinValue":
                        return 0d;

                    case "MaxValue":
                        return double.MaxValue;

                    case "ModifyPerClickValue":
                        return 0.1d;
                }
            }
            else if (currentValueType == typeof(decimal))
            {
                switch (propertyName)
                {
                    case "MinValue":
                        return 0m;

                    case "MaxValue":
                        return decimal.MaxValue;

                    case "ModifyPerClickValue":
                        return 0.1d;
                }
            }
            else if (currentValueType == typeof(float))
            {
                switch (propertyName)
                {
                    case "MinValue":
                        return 0f;

                    case "MaxValue":
                        return float.MaxValue;

                    case "ModifyPerClickValue":
                        return 0.1d;
                }
            }
            else if (currentValueType == typeof(TimeSpan))
            {
                switch (propertyName)
                {
                    case "MinValue":
                        return TimeSpan.Zero;

                    case "MaxValue":
                        return new TimeSpan(23, 59, 59);

                    case "ModifyPerClickValue":
                        return new TimeSpan(0, 0, 1);
                }
            }
            throw new ArgumentOutOfRangeException($"Für die Eigenschaft {propertyName} können keine Standardwerte für den Typ {currentValueType.Name} gefunden werden.");
        }

        private object TryConvertToAllowedType(string depValue)
        {
            if (currentValueType == typeof(int))
            {
                return Convert.ToInt32(depValue);
            }
            else if (currentValueType == typeof(double))
            {
                return Convert.ToDouble(depValue);
            }
            else if (currentValueType == typeof(decimal))
            {
                return Convert.ToDecimal(depValue);
            }
            else if (currentValueType == typeof(float))
            {
                return Convert.ToSingle(depValue);
            }
            else if (currentValueType == typeof(TimeSpan) && TimeOnly.TryParse(depValue, out TimeOnly time))
            {
                return new TimeSpan(time.Ticks);
            }
            throw new InvalidCastException($"Der Wert kann nicht in den Typ {currentValueType.Name} umgewandelt werden.");
        }

        #endregion ValueTypeDependingChecks

        #region DP Title

        public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(string), typeof(ValueUpDown),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, OnTitlePropertyChanged));

        private static void OnTitlePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (ValueUpDown)d;
            control.OnPropertyChanged(nameof(TitleVisibility));
        }

        public string Title
        {
            get
            {
                if (GetValue(MaxValueProperty) == null || (GetValue(MaxValueProperty) is string))
                {
                    SetValue(MaxValueProperty, GetCorrectTypeDependingValue(GetValue(MaxValueProperty), nameof(MaxValue)));
                }
                return (string)GetValue(TitleProperty);
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }

        #endregion DP Title

        public Visibility TitleVisibility => String.IsNullOrWhiteSpace(Title) ? Visibility.Collapsed : Visibility.Visible;

        public event PropertyChangedEventHandler? ValueChanged;

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ValueUpDown()
        {
            InitializeComponent();
        }

        private enum TimeCangeDirection
        {
            Up,
            Down
        }

        private void ButtonUp_Click(object sender, RoutedEventArgs e)
        {
            TryManipulateValue(TimeCangeDirection.Up);
        }

        private void ButtonDown_Click(object sender, RoutedEventArgs e)
        {
            TryManipulateValue(TimeCangeDirection.Down);
        }

        private void UserControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                TryManipulateValue(TimeCangeDirection.Up);
            }
            else
            {
                TryManipulateValue(TimeCangeDirection.Down);
            }
        }

        private void TryManipulateValue(TimeCangeDirection direction)
        {
            if (Value != null)
            {
                if (currentValueType == typeof(int))
                {
                    TryManipulateNumberValue<int>(direction);
                }
                else if (currentValueType == typeof(double))
                {
                    TryManipulateNumberValue<double>(direction);
                }
                else if (currentValueType == typeof(decimal))
                {
                    TryManipulateNumberValue<decimal>(direction);
                }
                else if (currentValueType == typeof(float))
                {
                    TryManipulateNumberValue<float>(direction);
                }
                else if (currentValueType == typeof(TimeSpan))
                {
                    TryManipulateTime(direction);
                }
                else
                {
                    throw new InvalidCastException($"Der Typ {currentValueType.Name} wird nicht unterstüzt.");
                }
            }
        }

        private void TryManipulateNumberValue<T>(TimeCangeDirection direction) where T : struct
        {
            dynamic addingValue = GetChangeValue<T>(direction);
            var ValueDummy = (T)Value + addingValue;
            if (ValueDummy < (T)MinValue)
            {
                Value = MinValue;
                return;
            }
            if (ValueDummy > (T)MaxValue)
            {
                Value = MaxValue;
                return;
            }
            Value = ValueDummy;
        }

        private T GetChangeValue<T>(TimeCangeDirection direction) where T : struct
        {
            dynamic changeValue = (T)ModifyPerClickValue;
            if (direction == TimeCangeDirection.Down) changeValue *= -1;
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                changeValue *= 10;
            }
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                changeValue *= 100;
            }
            return changeValue;
        }

        private void TryManipulateTime(TimeCangeDirection direction)
        {
            TimeSpan addingValue = GetChangeSpan(direction);
            var ValueDummy = new TimeSpan(((TimeSpan)Value).Ticks).Add(addingValue);
            if (ValueDummy < (TimeSpan)MinValue)
            {
                Value = MinValue;
                return;
            }
            if (ValueDummy > (TimeSpan)MaxValue)
            {
                Value = MaxValue;
                return;
            }
            Value = ValueDummy;
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

        private void ToolTip_Opened(object sender, RoutedEventArgs e)
        {
            ShiftModifyValueTextRun.Text = ShiftModifyValueText;
            CtrlModifyValueTextRun.Text = CtrlModifyValueText;
        }
    }
}