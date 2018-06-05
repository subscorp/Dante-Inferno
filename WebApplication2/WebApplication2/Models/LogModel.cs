using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class LogModel : WebModel
    {
        private LogEntry[] logs;

        public LogEntry[] Logs
        {
            get { return logs; }
            set { logs = value; }
        }

        public LogModel() : base()
        {
            logs = client.GetLogs();
        }
    }
}