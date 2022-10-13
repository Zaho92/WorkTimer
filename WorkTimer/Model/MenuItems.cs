using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using WorkTimer.View.Pages;

namespace WorkTimer.Model
{
    internal partial class MenuItems : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<MenuButtonModel>? _items;
        
        public MenuItems()
        {
            if (Items == null || Items.Count == 0)
            {
                Items = new ObservableCollection<MenuButtonModel>()
                {
                    new MenuButtonModel("Heute", new TodayWorkTimePage(),
                        @"/Images/Icons/stopwatch-multi-size.ico"),
                    new MenuButtonModel("Historie", new TodayWorkTimePage(),
                        @"/Images/Icons/calendar-multi-size.ico"),
                    new MenuButtonModel("Einstellungen", new TodayWorkTimePage(),
                        @"/Images/Icons/settings-multi-size.ico")
                };
            }
        }

        public bool SelectModel(MenuButtonModel buttonModel)
        {
            if (Items == null) return false;

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