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
        public string Message { get; set; }
        public string Type { get; set; }
        public DateTime? Time { get; set; }
        public string Color { get; set; }
    }
}
