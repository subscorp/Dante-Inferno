using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Commands;
using System.Collections.Generic;
using System.ComponentModel;

namespace GUI
{
    public class SettingViewModel : ViewModel, INotifyPropertyChanged
    {


        private SettingsModel sm;

        /*
         *                 settingsCol.Add(settings.OutputDir);
                settingsCol.Add(settings.LogSource);
                settingsCol.Add(settings.LogName);
                settingsCol.Add(settings.ThumbnailSize);
         */

        public string OutputDir
        {
            get { return _outputDir; }
            set { _outputDir = value; NotifyPropertyChanged(); }
        }

        public string LogSource
        {
            get { return _logSource; }
            set { _logSource = value; NotifyPropertyChanged(); }
        }

        public string LogName
        {
            get { return _logName; }
            set { _logName = value; NotifyPropertyChanged(); }
        }

        public string ThumbnailSize
        {
            get { return _thumbnailSize; }
            set { _thumbnailSize = value; NotifyPropertyChanged(); }
        }

        public ObservableCollection<string> Handlers { get; set; } = new ObservableCollection<string>();


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
                // remove from server
            }
        }

        private bool CanRemove(object obj)
        {
            return toRemove != null;
        }

        public SettingViewModel()
        {
            RemoveCommand = new DelegateCommand<object>(RemoveHandler, CanRemove);
        }

    }
}