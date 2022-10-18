using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WorkTimer.Controller;
using WorkTimer.ViewModel;

namespace WorkTimer.View.Windows
{
    public partial class MessageWindow : Window
    {
        internal MessageWindow(MessageController.MessageType type, string header, string message)
        {
            InitializeComponent();
            DataContext = new MessageWindowViewModel(type, header, message);
        }
    }
}
