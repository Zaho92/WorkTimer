﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using WorkTimer.Model;
using WorkTimer.ViewModel;

namespace WorkTimer.View.Pages
{
    public partial class TodayWorkTimePage : Page
    {
        public TodayWorkTimePage()
        {
            InitializeComponent();
            this.DataContext = new TodayWorkTimeViewModel();
        }
    }
}
