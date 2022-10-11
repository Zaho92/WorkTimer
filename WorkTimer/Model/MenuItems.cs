using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WorkTimer.View.Pages;

namespace WorkTimer.Model
{
    internal class MenuItems : ObservableObject
    {
        private readonly ObservableCollection<MenuButtonModel>? _items;
        public new ObservableCollection<MenuButtonModel> Items
        {
            get => _items;
            private init
            {
                _items = value;
                OnPropertyChanged("Items");
            }
        }
        
        public MenuItems()
        {
            if (_items == null || _items.Count == 0)
            {
                Items = new ObservableCollection<MenuButtonModel>()
                {
                    new MenuButtonModel("Heute", new TodayWorkTimePage(),
                        @"pack://application:,,,/Images/Icons/clock.ico"),
                    new MenuButtonModel("Historie", new TodayWorkTimePage(),
                        @"pack://application:,,,/Images/Icons/insight.ico"),
                    new MenuButtonModel("Einstellungen", new TodayWorkTimePage(),
                        @"pack://application:,,,/Images/Icons/settings.ico")
                };
            }
        }

        public bool SelectModel(MenuButtonModel buttonModel)
        {
            MenuButtonModel? containingModel = Items.FirstOrDefault(bm => bm == buttonModel);
            if (containingModel == null) return false;
            
            foreach (var buttonContent in Items)
            {
                buttonContent.IsSelected = false;
            }
            containingModel.IsSelected = true;

            return true;
        }
    }
}