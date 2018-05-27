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
        private static GUIClient instance = null;

        private GUIClient()
        {}

        public static GUIClient Instance
        {
            get
            {
                if (instance == null) return new GUIClient();
                return instance;
            }
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

        public bool Connected() { return client.Connected;  }

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

        public void HandleClient() { }

        public void Dispose()
        {
            client?.Close();
            client?.Dispose();
        }
    }
}
