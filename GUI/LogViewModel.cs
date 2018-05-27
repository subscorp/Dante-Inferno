using Communication;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GUI
{
    public class LogViewModel : ViewModel
    {
        private LogModel lm;

        public ObservableCollection<LogEntry> Logs
        {
            get { return lm.Logs; }

            set
            {
                NotifyPropertyChanged("Logs");
            }
        }

        public LogViewModel()
        {
            Logs = new ObservableCollection<LogEntry>();
            lm = new LogModel();
        }
    }
}
