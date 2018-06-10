using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;

namespace Communication
{

    /// <summary>
    /// Class GUIClient - for commmunication of GUI client with Image Service.
    /// </summary>
    public class GUIClient : IDisposable
    {
        private TcpClient client;
        private static GUIClient instance = null;

        /// <summary>
        /// Constructor - private, since the class is a singleton
        /// </summary>
        private GUIClient()
        {}

        /// <summary>
        /// Gets the instance of GUIClient
        /// </summary>
        /// <value>The instance.</value>
        public static GUIClient Instance
        {
            get
            {
                if (instance == null) return new GUIClient();
                return instance;
            }
        }

        public int GetNumberOfPhotos()
        {
            return sendCommand<int>(new CommandArgs()
            {
                CommandId = 4,
            });
    
        }

        public LogEntry[] GetLogs()
        {
            return sendCommand<LogEntry[]>(new CommandArgs()
            {
                CommandId = 2,
            });

        }

        public Settings GetSettings()
        {
            return sendCommand<Settings>(new CommandArgs()
            {
                CommandId = 1,
            });

        }

        public Settings RemoveHandler(string handler)
        {
            return sendCommand<Settings>(new CommandArgs()
            {
                CommandId = 3,
                Arg = handler
            });
        }

        public T sendCommand<T>(CommandArgs args)
        {  
            var stream = client.GetStream();
            var reader = new BinaryReader(stream);
            var writer = new BinaryWriter(stream);
            var mashehou = JsonConvert.SerializeObject(args);
            writer.Write(JsonConvert.SerializeObject(args));
            if (typeof(T) == typeof(object)) return default(T);
            var json = reader.ReadString();
            var obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }

        /// <summary>
        /// Connects the client to the server
        /// </summary>
        /// <returns>Task</returns>
        public Task Connect()
        {

            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            client = new TcpClient();
            return client.ConnectAsync(ep.Address, ep.Port);
        }

        /// <summary>
        /// Sends a command to the server of Image Service
        /// </summary>
        /// <param name="args">The arguments of command</param>
        /// <returns>Task.</returns>
        public Task SendCommand(CommandArgs args)
        {
            return SendCommand<object>(args);
        }

        /// </summary>
        /// <param name="args">The arguments of command.</param>
        /// <returns>Task.</returns>
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

        /// <summary>
        /// returns true if the GUIclient is connected, false otherwise.
        /// </summary>
        public bool Connected() { return client.Connected;  }

        /// <summary>
        /// Gets the settings from server.
        /// </summary>
        //public Task<Settings> GetSettings()
        //{
        //    return SendCommand<Settings>(new CommandArgs()
        //    {
        //        CommandId = 1,
        //    });
        //}

        /// <summary>
        /// Gets the logs from server.
        /// </summary>
        //public Task<LogEntry[]> GetLogs()
        //{
        //    return SendCommand<LogEntry[]>(new CommandArgs()
        //    {
        //        CommandId = 2
        //    });
        //}

        /// <summary>
        /// Removes a handler from server - the handler will not be followed anymore.
        /// </summary>
        /// <param name="handler">The handler.</param>
        //public Task RemoveHandler(string handler)
        //{
        //    return SendCommand(new CommandArgs()
        //    {
        //        CommandId = 3,
        //        Arg = handler
        //    });
        //}

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            client?.Close();
            client?.Client.Dispose();
        }
    }
}
