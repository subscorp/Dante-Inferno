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

namespace ImageService.ImageService.ImageService.Server
{
    class AppConfigHandlerV2 : IClientHandler
    {
        public void HandleClient(TcpClient client)
        {
            Settings settings = new Settings();
            string settingsStr = settings.ToJSON();
           
            string[] handlers = ConfigurationManager.AppSettings["Handler"].Split(';');
           
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

                            

//                            Console.WriteLine("sending handlersLength to the client");
//                            writer.Write(handlersLength);

//                            Console.WriteLine("sending handlers to the client");
//                            for (int j = 0; j < handlersLength; j++)
//                            {
//                                handler = handlers[j];
//                                writer.Write(handler);
//                            }

                        }
                        else
                        {
                            Console.WriteLine("sending log to the client\n");
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
