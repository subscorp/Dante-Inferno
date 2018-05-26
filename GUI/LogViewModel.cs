using Communication;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GUI
{
    public class LogViewModel : ViewModel
    {
        private LogModel lm;

        public Dictionary<string, string> TypeToColor
        {
            get;
            set;
        }

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
            TypeToColor = new Dictionary<string, string>();
            TypeToColor.Add("Information", "Green");
            TypeToColor.Add("Warning", "Yellow");
            TypeToColor.Add("Error", "Red");
        }

        //Clears logs from before the current starting of service
        public void clearLogs()
        {
            for (int i = Logs.Count-1; i > 0; i--)
            {
                if(Logs[i].Message == "ImageService stopped.")
                {
                    do
                    {
                        Logs.RemoveAt(i);
                        i--;
                    } while (i > -1);
                }
            }
        }
    }
}
