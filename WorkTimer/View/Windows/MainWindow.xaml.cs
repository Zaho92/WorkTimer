using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using WorkTimer.Model;

namespace WorkTimer.View.Windows
{
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