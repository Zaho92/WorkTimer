using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkTimer.View.Windows;

namespace WorkTimer.Controller
{
    internal static class MessageController
    {
        internal enum MessageType
        {
            Alarm,
            Info,
            YesNo,
            Warning,
            Error
        }

        public static void ShowMessageWindow(MessageType type, string header, string message)
        {
            MessageWindow msgWindow = new MessageWindow(type, header, message);
            msgWindow.ShowDialog();
        }

        public static void ShowMessageBubble(string header, string message)
        {

        }
    }
}
