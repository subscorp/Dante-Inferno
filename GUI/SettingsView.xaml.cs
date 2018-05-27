﻿using System;
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

namespace GUI
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {

        //public static readonly DependencyProperty SettingsViewModelProperty = DependencyProperty.Register(
        //    "SettingsViewModel", typeof(SettingViewModel), typeof(SettingsView), new PropertyMetadata(default(SettingViewModel)));

        //public SettingViewModel SettingsViewModel
        //{
        //    get { return (SettingViewModel) GetValue(SettingsViewModelProperty); }
        //    set { SetValue(SettingsViewModelProperty, value); }
        //}

        public SettingsView()
        {
            InitializeComponent();
            this.DataContext = new SettingViewModel();
        }

        //private void SettingsView_OnLoaded(object sender, RoutedEventArgs e)
        //{
            
        //}
    }
}
