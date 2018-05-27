using Communication;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GUI
{
    /// <summary>
    /// Class LogViewModel - for connection between log view and log model.
    /// </summary>
    /// <seealso cref="GUI.ViewModel" />
    public class LogViewModel : ViewModel
    {
        private LogModel lm;

        /// <summary>
        /// Gets or sets the logs.
        /// </summary>
        /// <value>The logs.</value>
        public ObservableCollection<LogEntry> Logs
        {
            get { return lm.Logs; }

            set
            {
                NotifyPropertyChanged("Logs");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogViewModel"/> class.
        /// </summary>
        public LogViewModel()
        {
            Logs = new ObservableCollection<LogEntry>();
            lm = new LogModel();
        }
    }
}
