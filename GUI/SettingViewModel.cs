using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Commands;
using System.Collections.Generic;
using System.ComponentModel;
using Communication;
using System.Threading;

namespace GUI
{
    public class SettingViewModel : ViewModel, INotifyPropertyChanged
    {

        private SettingsModel sm;
        
        public string OutputDir
        {
            get { return sm.OutputDir; }
            set { _outputDir = value; NotifyPropertyChanged(); }
        }

        public string LogSource
        {
            get { return sm.LogSource; }
            set { _logSource = value; NotifyPropertyChanged(); }
        }

        public string LogName
        {
            get { return sm.LogName; }
            set { _logName = value; NotifyPropertyChanged(); }
        }

        public string ThumbnailSize
        {
            get { return sm.ThumbnailSize; }
            set { _thumbnailSize = value; NotifyPropertyChanged(); }
        }

        public ObservableCollection<string> Handlers
        {
            get { return sm.Handlers; }
            set { }
        }

        private void CanExecuteRemoveChanged()
        {
            (RemoveCommand as DelegateCommand<object>)?.RaiseCanExecuteChanged();
        }

        public ICommand RemoveCommand { get; private set; }
        private string toRemove;
        private string _outputDir;
        private string _logSource;
        private string _logName;
        private string _thumbnailSize;


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
            while (toRemove != null)
            {
                Handlers.Remove(toRemove);
            }
        }

        private bool CanRemove(object obj)
        {
            return toRemove != null;
        }

        public SettingViewModel()
        {
            sm = new SettingsModel();

            //all settings change at once
            sm.PropertyChanged += delegate
             {
                 OutputDir = sm.OutputDir;
                 LogName = sm.LogName;
                 LogSource = sm.LogSource;
                 ThumbnailSize = sm.ThumbnailSize;
             };

            RemoveCommand = new DelegateCommand<object>(RemoveHandler, CanRemove);
        }

    }
}