using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    /// <summary>
    /// Class for model, which manages logs web page.
    /// </summary>
    public class LogModel : WebModel
    {
        //two lists - one of all logs, and one for the logs that would be presented at view
        private LogEntry[] logs = new LogEntry[0];
        private LogEntry[] logsPresented = new LogEntry[0];

        public LogEntry[] Logs
        {
            get { return logsPresented; }
            set { logs = value; }
        }

        //gets logs from client
        public void GetLogs()
        {
            if (client.Connected())
                logsPresented = logs = client.GetLogs();
        }

        //Removing all logs from presentation, except specified type
        public void Hide(string typeToShow)
        {
            //if the form is empty, present all logs
            if (typeToShow == "")
            {
                logsPresented = logs;
                return;
            }

            IList<LogEntry> newLogs = new List<LogEntry>();

            foreach (LogEntry l in logs)
            {
                //check whether the type should be shown
                if(l.Type == typeToShow || l.Type == typeToShow.ToUpper())
                {
                    newLogs.Add(l);
                }
            }

            logsPresented = newLogs.ToArray();
        }

        //Constructor
        public LogModel() : base()
        {
            GetLogs();
            logsPresented = logs;
        }
    }
}