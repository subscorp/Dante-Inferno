using Communication;
using System.Collections.ObjectModel;

namespace GUI
{
    internal class LogModel
    {
        //private mashehou shekashour leTCP

        public ObservableCollection<LogEntry> Logs
        {
            get;
            set;
        }

        public LogModel()
        {
            Logs = new ObservableCollection<LogEntry>();

            LogEntry l1 = new LogEntry();
            l1.Message = "Shalom lekha debil";
            l1.Type = "INFO";

            LogEntry l2 = new LogEntry();
            l2.Message = "Shalom lekha evil";
            l2.Type = "WARNING";
            Logs.Add(l1);
            Logs.Add(l2);
            //TODO mashehou shekashour laTCP
        }
    }
}