using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Linq;
using System.Windows;
using WorkTimer.Controller;
using WorkTimer.View.Windows;

namespace WorkTimer.ViewModel
{
    internal partial class MessageWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _windowTitle;

        [ObservableProperty]
        private string _messageTypeIconPath;

        [ObservableProperty]
        private string _messageHeaderText;

        [ObservableProperty]
        private string _messageText;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(OkButtonVisibility))]
        [NotifyPropertyChangedFor(nameof(AlarmButtonVisibility))]
        [NotifyPropertyChangedFor(nameof(YesNoButtonVisibility))]
        private MessageController.MessageType _thisType;

        public Visibility OkButtonVisibility => (ThisType != MessageController.MessageType.Alarm && ThisType != MessageController.MessageType.YesNo) ? Visibility.Visible : Visibility.Collapsed;
        public Visibility AlarmButtonVisibility => (ThisType == MessageController.MessageType.Alarm) ? Visibility.Visible : Visibility.Collapsed;
        public Visibility YesNoButtonVisibility => (ThisType == MessageController.MessageType.YesNo) ? Visibility.Visible : Visibility.Collapsed;

        public MessageWindowViewModel(MessageController.MessageType type, string header, string message)
        {
            WindowTitle = header;
            ThisType = type;
            MessageTypeIconPath = GetIconPathFromMessageType();
            MessageHeaderText = header;
            MessageText = message;
        }

        private string GetIconPathFromMessageType()
        {
            string iconPath = @"/Images/Icons/";
            switch (ThisType)
            {
                case MessageController.MessageType.Alarm:
                    return iconPath + "alarm-multi-size.ico";

                case MessageController.MessageType.Info:
                    return iconPath + "info-multi-size.ico";

                case MessageController.MessageType.YesNo:
                    return iconPath + "question-multi-size.ico";

                case MessageController.MessageType.Warning:
                    return iconPath + "warning-multi-size.ico";

                case MessageController.MessageType.Error:
                    return iconPath + "error-multi-size.ico";

                default:
                    throw new NotImplementedException("MessageType unbekannt");
            }
        }

        [RelayCommand]
        public void OkButtonClick()
        {
            CloseMessageWindow();
        }

        [RelayCommand]
        public void SnoozeButtonClick()
        {
            CloseMessageWindow();
        }

        [RelayCommand]
        public void CancelAlarmButtonClick()
        {
            CloseMessageWindow();
        }

        [RelayCommand]
        public void YesButtonClick()
        {
            CloseMessageWindow();
        }

        [RelayCommand]
        public void NoButtonClick()
        {
            CloseMessageWindow();
        }

        private void CloseMessageWindow()
        {
            MessageWindow? thisWindow = Application.Current.Windows.OfType<System.Windows.Window>().SingleOrDefault(x => x.IsActive) as MessageWindow;
            if (thisWindow != null)
            {
                thisWindow.Close();
            }
        }
    }
}