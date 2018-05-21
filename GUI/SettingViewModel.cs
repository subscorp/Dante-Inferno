using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace GUI
{
    internal class SettingViewModel : ViewModel
    {
        /*
        private SettingsModel sm;

       

        public ObservableCollection<string> Settings
        {
            get => sm.settings;
            set
            {
                NotifyPropertyChanged("Settings");
            }
        }

        public ICommand RemoveCommand { get; private set; }

        public ObservableCollection<string> Handlers
        {
            get => sm.handlers;
            set
            {
                NotifyPropertyChanged("Handlers");
            }
        }

        private void RemoveHandler(object obj)
        {
            Console.Write("I remove handler!!!");
            Handlers.Remove(obj as string);
        }

        private bool CanRemove(object obj)
        {
            return true;
        }

        public SettingViewModel()
        {
            RemoveCommand =  new DelegateCommand<object>(RemoveHandler, CanRemove);
            sm = new SettingsModel();
        }
        */
    }
}