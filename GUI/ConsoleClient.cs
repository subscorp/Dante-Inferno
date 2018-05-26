﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;
using Communication;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace GUI
{
    class ConsoleClient : IClient
    {
        private TcpClient client;

        private ConsoleClient()
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

        //internal string getSettings()
        //{
        //    using (NetworkStream stream = client.GetStream())
        //    using (BinaryReader reader = new BinaryReader(stream))
        //    using (BinaryWriter writer = new BinaryWriter(stream))
        //    {
        //        writer.Write(JsonConvert.SerializeObject(new CommandArgs()
        //        {
        //            CommandId = 1
        //        }));
        //        Console.WriteLine("Settings:");
        //        return reader.ReadString();
        //    }
        //}

        //public void SettingsCommunication()
        //{

        //}
        public event PropertyChangedEventHandler PropertyChanged; 

        //public void HandleClient() { }
        public Settings Settings
        {
            get;
            set;
        }

        public ObservableCollection<LogEntry> Logs
        {
            get;
            set;
        }

        public void HandleClient()
        {
            Console.WriteLine("ani metapel besettingim");
            Settings = new Settings();
            string settingsStr;
            int numHandlers;

            string logStr;
            LogEntry entry;

            

            using (NetworkStream stream = client.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                if(client.Connected)
                {
                    Console.WriteLine("Settings:");
                    settingsStr = reader.ReadString();
                    Settings = Settings.FromJSON(settingsStr);
                    numHandlers = Settings.Handlers.Count;

                    var json = reader.ReadString();
                    var arr = JsonConvert.DeserializeObject<LogEntry[]>(json);

                    foreach (var obj in arr)
                    {
                        LogEntry le = new LogEntry();
                        le.Message = obj.Message;
                        le.Type = obj.Type;
                        Logs.Add(le);
                    }
                }
            //    while (client.Connected)
            //    {
            //        Console.WriteLine("please choose operation: enter 1 for Settings or 2 for Log");
            //        int num = int.Parse(Console.ReadLine());


            //        if (num == 1)
            //        {
            //            writer.Write(JsonConvert.SerializeObject(new CommandArgs()
            //            {
            //                CommandId = num
            //            }));
            //            Console.WriteLine("Settings:");
            //            settingsStr = reader.ReadString();
            //            settings = Settings.FromJSON(settingsStr);
            //            numHandlers = settings.Handlers.Count;

            //            Console.WriteLine("Output Directory: {0}", settings.OutputDir);

            //            Console.WriteLine("Source Name: {0}", settings.LogSource);

            //            Console.WriteLine("Log Name: {0}", settings.LogName);

            //            Console.WriteLine("Thumbnail Size: {0}", settings.ThumbnailSize);

            //            Console.WriteLine("Handlers:");

            //            for (int i = 0; i < numHandlers; i++)
            //                Console.WriteLine(settings.Handlers[i]);
            //        }

            //        else if (num == 2)
            //        {
            //            writer.Write(JsonConvert.SerializeObject(new CommandArgs()
            //            {
            //                CommandId = num
            //            }));
            //            Console.WriteLine("Log:");
            //            var json = reader.ReadString();
            //            var arr = JsonConvert.DeserializeObject<LogEntry[]>(json);

            //            foreach (var obj in arr)
            //            {
            //                Console.WriteLine(obj.Type);
            //                Console.WriteLine(obj.Message);
            //            }

            //        }
            //        else if (num == 3)
            //        {
            //            Console.WriteLine("Path:");
            //            var path = Console.ReadLine();
            //            var json = JsonConvert.SerializeObject(new CommandArgs()
            //            {
            //                CommandId = 3,
            //                Arg = path
            //            });
            //            writer.Write(json);
            //        }
            //        Console.WriteLine();
            //    }
            //}
            //client.Close();
        }
    }
}
