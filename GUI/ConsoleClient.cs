using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;
using Communication;

namespace GUI
{
    public class ConsoleClient : IClient
    {
        private TcpClient client;

        public ConsoleClient()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            client = new TcpClient();
            client.Connect(ep);
            Console.WriteLine("You are connected\n");
        }

        private static ConsoleClient instance;

        public static ConsoleClient Instance
        {
            get
            {
                if (instance == null) return new ConsoleClient();
                return instance;
            }
        }

        public void SettingsCommunication()
        {

        }

        public void HandleClient()
        {
            Settings settings = new Settings();
            string settingsStr;
            int numHandlers;

            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            TcpClient client = new TcpClient();
            client.Connect(ep);
            Console.WriteLine("You are connected\n");

            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                //getting and printing the Settings
                Console.WriteLine("Settings:");
                settingsStr = reader.ReadString();
                settings = Settings.FromJSON(settingsStr);
                numHandlers = settings.Handlers.Count;
                Console.WriteLine("Output Directory: {0}", settings.OutputDir);
                Console.WriteLine("Source Name: {0}", settings.LogSource);
                Console.WriteLine("Log Name: {0}", settings.LogName);
                Console.WriteLine("Thumbnail Size: {0}", settings.ThumbnailSize);
                Console.WriteLine("Handlers:");
                for (int i = 0; i < numHandlers; i++)
                    Console.WriteLine(settings.Handlers[i]);

                //getting and printing the log
                Console.WriteLine("Log:");
                var json = reader.ReadString();
                var arr = JsonConvert.DeserializeObject<LogEntry[]>(json);
                foreach (var obj in arr)
                {
                    Console.WriteLine(obj.Type);
                    Console.WriteLine(obj.Message);
                }

                while (client.Connected)
                {
                    Console.WriteLine("please choose operation: enter 1 for Settings or 2 for Log");
                    int num = int.Parse(Console.ReadLine());


                    if (num == 1)
                    {
                        writer.Write(JsonConvert.SerializeObject(new CommandArgs()
                        {
                            CommandId = num
                        }));
                        Console.WriteLine("Settings:");
                        settingsStr = reader.ReadString();
                        settings = Settings.FromJSON(settingsStr);
                        numHandlers = settings.Handlers.Count;

                        Console.WriteLine("Output Directory: {0}", settings.OutputDir);

                        Console.WriteLine("Source Name: {0}", settings.LogSource);

                        Console.WriteLine("Log Name: {0}", settings.LogName);

                        Console.WriteLine("Thumbnail Size: {0}", settings.ThumbnailSize);

                        Console.WriteLine("Handlers:");

                        for (int i = 0; i < numHandlers; i++)
                            Console.WriteLine(settings.Handlers[i]);
                    }

                    else if (num == 2)
                    {
                        writer.Write(JsonConvert.SerializeObject(new CommandArgs()
                        {
                            CommandId = num
                        }));
                        Console.WriteLine("Log:");
                        var json2 = reader.ReadString();
                        var arr2 = JsonConvert.DeserializeObject<LogEntry[]>(json2);

                        foreach (var obj in arr2)
                        {
                            Console.WriteLine(obj.Type);
                            Console.WriteLine(obj.Message);
                        }

                    }
                    else if (num == 3)
                    {
                        Console.WriteLine("Path:");
                        var path = Console.ReadLine();
                        var json3 = JsonConvert.SerializeObject(new CommandArgs()
                        {
                            CommandId = 3,
                            Arg = path
                        });
                        writer.Write(json3);
                    }
                    Console.WriteLine();
                }
            }
            client.Close();
        }
    }
}
