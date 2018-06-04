using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class LogModel
    {
        private GUIClient client;
        private LogEntry[] logs;

        public LogEntry[] Logs
        {
            get { return logs; }
            set { logs = value; }
        }

        public LogModel()
        {
            client = GUIClient.Instance;
            client.Connect();

            logs = client.GetLogs();
        }
    }
}