using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace WorkTimer.Model
{
    public partial class MenuButtonModel : ObservableObject
    {
        public MenuButtonModel(string menuTitle, Page framePage, string? menuImageSource = null)
        {
            MenuTitle = menuTitle;
            FramePage = framePage;
            MenuImageSource = menuImageSource ?? @"/Images/Icons/menu-multi-size.ico";
            IsSelected = false;
        }

        [ObservableProperty]
        private string _menuTitle;

        [ObservableProperty]
        private Page _framePage;

        [ObservableProperty]
        private string _menuImageSource;

        [ObservableProperty]
        private bool _isSelected;
    }
}
