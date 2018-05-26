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

            LogEntry l0 = new LogEntry();
            l0.Message = "Quantic Dream surpasses everything it has done before with Detroit: Become Human.\nA huge script allows for a thrilling story to have multiple layers that, at the same time, deal with topics such as slavery, the human condition or the concept of identity.\nA real masterpiece of the genre.\nbut bla bla\nblabla\nyadayadayada oh\n\nblablablabla";
            l0.Type = "INFO";
            l0.Color = "Green";

            LogEntry l1 = new LogEntry();
            l1.Message = "Shalom lekha debil";
            l1.Type = "INFO";
            l1.Color = "Green";

            LogEntry l2 = new LogEntry();
            l2.Message = "Shalom lekha evil";
            l2.Type = "WARNING";
            l2.Color = "Yellow";
            
            LogEntry l3 = new LogEntry();
            l3.Message = "Shalom lekha kasil";
            l3.Type = "ERROR";
            l3.Color = "Red";

            LogEntry l4 = new LogEntry();
            l4.Message = "bye lekha debil";
            l4.Type = "INFO";
            l4.Color = "Green";

     //       Logs.Add(l0);
            Logs.Add(l1);
            Logs.Add(l2);
            Logs.Add(l3);
            Logs.Add(l4);
            //TODO mashehou shekashour laTCP
        }
    }
}