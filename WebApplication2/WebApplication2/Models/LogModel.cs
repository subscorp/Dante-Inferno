using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class LogModel : WebModel
    {
        private LogEntry[] logs = new LogEntry[0];

        public LogEntry[] Logs
        {
            get { return logs; }
            set { logs = value; }
        }

        public void GetLogs()
        {
            if (client.Connected())
                logs = client.GetLogs();
        }

        public LogModel() : base()
        {
            GetLogs();
        }
    }
}