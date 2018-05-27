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
    /// <summary>
    /// Class SettingViewModel - to manage conection between setting view and setting model
    /// </summary>
    /// <seealso cref="GUI.ViewModel" />
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class SettingViewModel : ViewModel, INotifyPropertyChanged
    {

        private SettingsModel sm;

        /// <summary>
        /// Gets or sets the output dir.
        /// </summary>
        /// <value>The output dir.</value>
        public string OutputDir
        {
            get { return sm.OutputDir; }
            set { _outputDir = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the log source.
        /// </summary>
        /// <value>The log source.</value>
        public string LogSource
        {
            get { return sm.LogSource; }
            set { _logSource = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the name of the log.
        /// </summary>
        /// <value>The name of the log.</value>
        public string LogName
        {
            get { return sm.LogName; }
            set { _logName = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the size of the thumbnail.
        /// </summary>
        /// <value>The size of the thumbnail.</value>
        public string ThumbnailSize
        {
            get { return sm.ThumbnailSize; }
            set { _thumbnailSize = value; NotifyPropertyChanged(); }
        }

        /// <summary>
        /// Gets or sets the handlers.
        /// </summary>
        /// <value>The handlers.</value>
        public ObservableCollection<string> Handlers
        {
            get { return sm.Handlers; }
            set { }
        }

        // Updates the view that it can/can't remove handlers
        private void CanExecuteRemoveChanged()
        {
            (RemoveCommand as DelegateCommand<object>)?.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Gets the remove handler command.
        /// </summary>
        /// <value>The remove command.</value>
        public ICommand RemoveCommand { get; private set; }
        private string toRemove;
        private string _outputDir;
        private string _logSource;
        private string _logName;
        private string _thumbnailSize;


        /// <summary>
        /// Gets or sets the remove command.
        /// </summary>
        /// <value>To remove.</value>
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

        /// <summary>
        /// Removes selected handlers
        /// </summary>
        private void RemoveHandler(object obj)
        {
            while (toRemove != null)
            {
                Handlers.Remove(toRemove);
            }
        }

        /// <summary>
        /// boolean - tells whether handlers can be removed
        /// </summary>
        private bool CanRemove(object obj)
        {
            return toRemove != null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingViewModel"/> class.
        /// </summary>
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