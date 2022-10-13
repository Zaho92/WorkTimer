using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WorkTimer.Model;
using WorkTimer.View.Pages;

namespace WorkTimer.View.Windows
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MenuItems _thisMenuItems;

        public MainWindow()
        {
            InitializeComponent();
            _thisMenuItems = new MenuItems();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            MenuListBox.DataContext = _thisMenuItems;
            SetMenuButtonAsClicked(_thisMenuItems.Items.First());
        }

        private void MenuButton_OnClick(object sender, RoutedEventArgs e)
        {
            MenuButtonModel? thisButtonContent = ((sender as ToggleButton)?.DataContext as MenuButtonModel);
            SetMenuButtonAsClicked(thisButtonContent);
        }

        private void SetMenuButtonAsClicked(MenuButtonModel? clickedButtonModel)
        {
            if (clickedButtonModel == null) return;
            _thisMenuItems.SelectModel(clickedButtonModel);
            ContentFrame.Navigate(clickedButtonModel.FramePage);
        }
    }
}