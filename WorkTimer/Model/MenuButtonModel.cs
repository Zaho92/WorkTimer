using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WorkTimer.Model
{
    public class MenuButtonModel : ObservableObject
    {
        public MenuButtonModel(string menuTitle, Page framePage, string? menuImageSource = null)
        {
            _menuTitle = menuTitle;
            _framePage = framePage;
            _menuImageSource = menuImageSource ?? @"pack://application:,,,/Images/Icons/menu.ico";
            _isSelected = false;
        }

        private string _menuTitle;

        public string MenuTitle
        {
            get { return _menuTitle; }
            set
            {
                if (value != _menuTitle)
                {
                    _menuTitle = value;
                    OnPropertyChanged("MenuTitle");
                }
            }
        }

        private Page _framePage;

        public Page FramePage
        {
            get { return _framePage; }
            set
            {
                if (value != _framePage)
                {
                    _framePage = value;
                    OnPropertyChanged("FramePage");
                }
            }
        }

        private string _menuImageSource;

        public string MenuImageSource
        {
            get { return _menuImageSource; }
            set
            {
                if (value != _menuImageSource)
                {
                    _menuImageSource = value;
                    OnPropertyChanged("MenuImageSource");
                }
            }
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (value != _isSelected)
                {
                    _isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }
    }
}
