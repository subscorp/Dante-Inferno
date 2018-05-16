using System;
using System.Collections.Generic;
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

namespace ImageService.ImageService.ImageService.Server
{
    class ConsoleClientHandler : IClientHandler
    {
        public void HandleClient(TcpClient client)
        {
           
            //appConfig variables
            string logSource = ConfigurationManager.AppSettings["SourceName"];
            string logName = ConfigurationManager.AppSettings["LogName"];
            string[] handlers = ConfigurationManager.AppSettings["Handler"].Split(';');
            int handlersLength = handlers.Length;
            int eventLogLength;
            string handler;
            string outputDir = ConfigurationManager.AppSettings["OutputDir"];
            string thumbnailSize = ConfigurationManager.AppSettings["ThumbnailSize"];

            //log variables
            EventLog eventLog1 = new EventLog();

            //initialize the EventLogger with values from configuration file
            ((ISupportInitialize)(eventLog1)).BeginInit();
             
            if (!EventLog.SourceExists(logSource))
            {
                EventLog.CreateEventSource(logSource, logName);
            }
            eventLog1.Source = logSource;
            eventLog1.Log = logName;
            ((ISupportInitialize)(eventLog1)).EndInit();

            new Task(() =>
            {
                using (NetworkStream stream = client.GetStream())
                using (BinaryReader reader = new BinaryReader(stream))
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    while (true)
                    {
                        int num = reader.ReadInt32();
                        if(num == 1)
                        {
                            Console.WriteLine("sending settings to the client:\n");

                            Console.WriteLine("sending outputDir to the client");
                            writer.Write(outputDir);

                            Console.WriteLine("sending logSource to the client");
                            writer.Write(logSource);

                            Console.WriteLine("sending logName to the client");
                            writer.Write(logName);

                            Console.WriteLine("sending thumbnailSize to the client");
                            writer.Write(thumbnailSize);

                            Console.WriteLine("sending handlersLength to the client");
                            writer.Write(handlersLength);

                            Console.WriteLine("sending handlers to the client");
                            for (int j = 0; j < handlersLength; j++)
                            {
                                handler = handlers[j];
                                writer.Write(handler);
                            }

                        }
                        else
                        {
                            Console.WriteLine("sending log to the client\n");
                            eventLogLength = eventLog1.Entries.Count;
                            writer.Write(eventLogLength);
                            foreach (EventLogEntry log in eventLog1.Entries)
                            {
                                writer.Write(log.Message);
                            }
                        }

                        Console.WriteLine();
                    }

                }

                /*
                string commandLine = reader.ReadLine();
                Console.WriteLine("Got command: {0}", commandLine);
                string result = ExecuteCommand(commandLine, client);
                writer.Write(result);
                */
                client.Close();
                Console.WriteLine("client disconnected");
            }).Start();
        }

        private string ExecuteCommand(string commandLine, TcpClient client)
        {
            throw new NotImplementedException();
        }
    }
}
