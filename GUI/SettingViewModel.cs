using System;
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

        public ConsoleClient Client
        {
            get;
            private set;
        }

        public ObservableCollection<string> Settings
        {
            get { return sm.settings; }
            set
            {
                sm.settings = value;
            }
        }

        public ObservableCollection<string> Handlers
        {
            get { return sm.handlers; }
            private set
            {
                sm.handlers = value;
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
                toRemove = value;
                NotifyPropertyChanged("ToRemove");
                CanExecuteRemoveChanged();  
            }
        }

        private void RemoveHandler(object obj)
        {
            Console.WriteLine("I remove handler!!!");
            while (toRemove != null)
            {
                Handlers.Remove(toRemove);
                // remove from server
            }
        }

        private bool CanRemove(object obj)
        {
            return toRemove != null;
        }

        public SettingViewModel()
        {
            RemoveCommand =  new DelegateCommand<object>(RemoveHandler, CanRemove);
            sm = new SettingsModel();
            sm.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged(e.PropertyName);
            };
        }
        
    }
}