﻿using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Commands;
using System.Collections.Generic;
using System.ComponentModel;

namespace GUI
{
    internal class SettingViewModel : ViewModel
    {
        
        private SettingsModel sm;

        public ObservableCollection<string> Settings
        {
            get => sm.settings;
            set
            {
                NotifyPropertyChanged("Settings");
            }
        }

        public ObservableCollection<string> Handlers
        {
            get => sm.handlers;
            set
            {
                NotifyPropertyChanged("Handlers");
            }
        }

        private void CanExecuteRemoveChanged()
        {
            (RemoveCommand as DelegateCommand<object>)?.RaiseCanExecuteChanged();
        }

        public ICommand RemoveCommand { get; private set; }
        private string toRemove;
        

        public string ToRemove
        {
            get { return toRemove; }
            set
            {
                Console.WriteLine("i am here and i want food and " + value);
                toRemove = value;
                NotifyPropertyChanged("ToRemove");
                CanExecuteRemoveChanged();
                ;
            }
        }

        private void RemoveHandler(object obj)
        {
            Console.WriteLine("I remove handler!!!");
            
            Console.Write(toRemove);
            Handlers.Remove(toRemove);
        }

        private bool CanRemove(object obj)
        {
            return toRemove != null;
        }

        public SettingViewModel()
        {
            RemoveCommand =  new DelegateCommand<object>(RemoveHandler, CanRemove);
            sm = new SettingsModel();
        }
        
    }
}