using Communication;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GUI
{
    public class LogViewModel : ViewModel
    {

        public ObservableCollection<LogEntry> Logs
        {
            get;
            set;
        } = new ObservableCollection<LogEntry>()
        {
            new LogEntry()
        };

        public LogViewModel()
        {

        }
        
    }
}
