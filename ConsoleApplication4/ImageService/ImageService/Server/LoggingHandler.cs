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

namespace ImageService.ImageService.ImageService.Server
{
    class LoggingHandler : IClientHandler
    {
        public void HandleClient(TcpClient client)
        {

            ImgService service = new ImgService();
            EventLog log = new EventLog();
            EventLogEntryCollection entries = log.Entries;
            string logStr = entries.ToString();

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
                        }
                        else
                        {
                            Console.WriteLine("sending log to the client\n");
                            writer.Write(logStr);
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
