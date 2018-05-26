using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication
{

    public class LogEntry
    {
        private Dictionary<string, string> typeToColor = new Dictionary<string, string>()
        {
            {"INFO", "Green" },
            {"ERROR", "Red" },
            {"WARNING", "Yellow"}
        };

        public string Message { get; set; }
        public string Type { get; set; }
        public DateTime? Time { get; set; }
        public string Color { get; set; }

        public string ToJSON()
        {
            JObject LogObj = new JObject();
            LogObj["Message"] = Message;
            LogObj["Type"] = Type;
            return LogObj.ToString();
        }

        public static LogEntry FromJSON(string str)
        {
            LogEntry logEntry = new LogEntry();
            JObject appConfigObj = JObject.Parse(str);
            logEntry.Message = (string)appConfigObj["Message"];
            logEntry.Type = (string)appConfigObj["Type"];
            logEntry.Color = logEntry.typeToColor[logEntry.Type];
            return logEntry;
        }
    }
}
