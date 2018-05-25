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

            EventLog eventLog1 = LogContainer.Log;

            new Task(() =>
            {
                using (NetworkStream stream = client.GetStream())
                using (BinaryReader reader = new BinaryReader(stream))
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    Console.WriteLine("sending settings to the client:\n");
                    writer.Write(settings.ToJSON());

                    Console.WriteLine("sending log to the client\n");
                    var arr = eventLog1.Entries.Cast<EventLogEntry>();

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
                        writer.Write(JsonConvert.SerializeObject(logEntries.ToArray(), Formatting.Indented));

                        while (client.Connected)
                    {
                        CommandArgs cmd;
                        try
                        {
                            var json = reader.ReadString();
                            cmd = JsonConvert.DeserializeObject<CommandArgs>(json);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            return;
                        }


                        if (cmd.CommandId == 1)
                        {
                            Console.WriteLine("sending settings to the client:\n");
                            writer.Write(settings.ToJSON());
                        }
                        else if (cmd.CommandId == 2)
                        {
                            Console.WriteLine("sending log to the client\n");
                            arr = eventLog1.Entries.Cast<EventLogEntry>();

                            logEntries = new List<LogEntry>();

                            foreach (var log in arr)
                            {
                                var msg = log.Message;
                                var type = log.EntryType;
                                var logEntry = new LogEntry();
                                logEntry.Message = msg;
                                logEntry.Type = type.ToString();
                                logEntries.Add(logEntry);
                            }

                            numLogEntries = logEntries.Count;
                            writer.Write(JsonConvert.SerializeObject(logEntries.ToArray(), Formatting.Indented));
                        }
                        else if (cmd.CommandId == 3)
                        {
                            settings.Handlers.Remove(cmd.Arg);
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
