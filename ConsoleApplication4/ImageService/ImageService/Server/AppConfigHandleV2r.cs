using System;

using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Configuration;
using System.Diagnostics;
using System.ComponentModel;
using Communication;

namespace ImageService.ImageService.ImageService.Server
{
    class AppConfigHandlerV2 : IClientHandler
    {
        private Settings settings;
        public AppConfigHandlerV2(Settings settings)
        {
            this.settings = settings;
        }

        public void HandleClient(TcpClient client)
        {
            string settingsStr = settings.ToJSON();

            EventLog eventLog1 = LogContainer.Log;

            new Task(() =>
            {
                using (NetworkStream stream = client.GetStream())
                using (BinaryReader reader = new BinaryReader(stream))
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    while (true)
                    {
                        int num = reader.ReadInt32();
                        if (num == 1)
                        {
                            Console.WriteLine("sending settings to the client:\n");
                            writer.Write(settingsStr);
                        }
                        else
                        {
                            Console.WriteLine("sending log to the client\n");
                            var arr = eventLog1.Entries.Cast<EventLogEntry>().ToArray();

                            var logEntries = new List<LogEntry>();

                            foreach (var entry in arr)
                            {    
                                var msg = entry.Message;
                                var type = entry.EntryType;
                                var logEntry = new LogEntry();
                                logEntry.Message = msg;
                                logEntry.Type = type.ToString();
                                logEntries.Add(logEntry);
                            }

                            int numLogEntries = logEntries.Count;
                            writer.Write(numLogEntries);
                            foreach (var logEntry in logEntries)
                                writer.Write(logEntry.ToJSON());
                        }

                        Console.WriteLine();
                    }

                }
            }).Start();
        }

        private string ExecuteCommand(string commandLine, TcpClient client)
        {
            throw new NotImplementedException();
        }
    }
}
