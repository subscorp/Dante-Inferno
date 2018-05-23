using Communication;
using System.Collections.ObjectModel;

namespace GUI
{
    internal class LogViewModel : ViewModel
    {
        private LogModel lm;

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
        }
    }
}
