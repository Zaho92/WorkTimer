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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkTimer.View.ViewModel;

namespace WorkTimer.View.Pages
{
    /// <summary>
    /// Interaktionslogik für TodayWorkTimePage.xaml
    /// </summary>
    public partial class TodayWorkTimePage : Page
    {
        public int RandomValue { get; set; }
        public TodayWorkTimePage()
        {
            InitializeComponent();
            this.DataContext = new TodayTimeViewModel();
        }

        private void TodayWorkTimePage_OnLoaded(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            RandomValue = random.Next();
        }
    }
}
