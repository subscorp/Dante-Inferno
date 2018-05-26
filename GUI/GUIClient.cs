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

    public class GUIClient : IClient
    {
        private TcpClient client;
        public GUIClient()
        {

        }

        public Task Connect()
        {

            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            client = new TcpClient();
            return client.ConnectAsync(ep.Address, ep.Port);
        }

        public Task SendCommand(CommandArgs args)
        {
            return SendCommand<object>(args);
        }

        private Task<TReturn> SendCommand<TReturn>(CommandArgs args)
        {
            return Task.Run(() =>
            {
                var stream = client.GetStream();
                var reader = new BinaryReader(stream);
                var writer = new BinaryWriter(stream);
                writer.Write(JsonConvert.SerializeObject(args));
                if (typeof(TReturn) == typeof(object)) return default(TReturn);
                var json = reader.ReadString();
                var obj = JsonConvert.DeserializeObject<TReturn>(json);
                return obj;
                
            });
        }

        public Task<Settings> GetSettings()
        {
            return SendCommand<Settings>(new CommandArgs()
            {
                CommandId = 1,
            });
        }

        public Task<LogEntry[]> GetLogs()
        {
            return SendCommand<LogEntry[]>(new CommandArgs()
            {
                CommandId = 2
            });
        }

        public Task RemoveHandler(string handler)
        {
            return SendCommand(new CommandArgs()
            {
                CommandId = 3,
                Arg = handler
            });
        }

        public void HandleClient()
        {
            Settings settings = new Settings();
            string settingsStr;
            int numHandlers;

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

        public void Dispose()
        {
            client?.Close();
            client?.Dispose();
        }
    }
}
