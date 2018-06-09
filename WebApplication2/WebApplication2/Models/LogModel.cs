﻿using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class LogModel : WebModel
    {
        private LogEntry[] logs = new LogEntry[0];
        private LogEntry[] logsPresented = new LogEntry[0];

        public LogEntry[] Logs
        {
            get { return logsPresented; }
            set { logs = value; }
        }

        public void GetLogs()
        {
            if (client.Connected())
                logs = client.GetLogs();
        }

        public void Hide(string type)
        {
            IList<LogEntry> newLogs = new List<LogEntry>();
            foreach(LogEntry l in logs)
            {
                if(l.Type == type)
                {
                    newLogs.Add(l);
                }
            }

            logsPresented = newLogs.ToArray();
        }

        public LogModel() : base()
        {
            GetLogs();
            logsPresented = logs;
        }
    }
}