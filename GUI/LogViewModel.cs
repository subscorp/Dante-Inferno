using Communication;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GUI
{
    internal class LogViewModel : ViewModel
    {
        private LogModel lm;

        public Dictionary<string, string> TypeToColor
        {
            get;
            set;
        }

        public ObservableCollection<LogEntry> Logs
        {
            get => lm.Logs;

            set
            {
                NotifyPropertyChanged("Logs");
            }
        }

        public LogViewModel()
        {
            Logs = new ObservableCollection<LogEntry>();
            lm = new LogModel();
            //TypeToColor = new Dictionary<string, string>();
            //TypeToColor.Add("INFO", "Green");
            //TypeToColor.Add("WARNING", "Yellow");
            //TypeToColor.Add("ERROR", "Red");
        }
    }
}
