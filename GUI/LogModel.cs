using Communication;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GUI
{
    internal class LogModel
    {
        //private ConsoleClient client;
        private Dictionary<string, string> TypeToColor;

        public ObservableCollection<LogEntry> Logs
        {
            get;
            set;
        }

        public LogModel()
        {
            //client = ConsoleClient.Instance;

            TypeToColor = new Dictionary<string, string>();
            TypeToColor.Add("INFO", "Green");
            TypeToColor.Add("WARNING", "Yellow");
            TypeToColor.Add("ERROR", "Red");

            Logs = new ObservableCollection<LogEntry>();

            foreach(LogEntry le in Logs)
            {
                Console.WriteLine(le.Type);
                le.Color = TypeToColor[le.Type];
                Logs.Add(le);
            }
            LogEntry l1 = new LogEntry();
            l1.Message = "Shalom lekha debil";
            l1.Type = "INFO";
            l1.Color = "Green";

            LogEntry l2 = new LogEntry();
            l2.Message = "Shalom lekha evil";
            l2.Type = "WARNING";
            l2.Color = "Yellow";
            Logs.Add(l1);
            Logs.Add(l2);
            //TODO mashehou shekashour laTCP
        }
    }
}