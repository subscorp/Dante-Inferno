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

        /// <summary>
        /// Gets the number of photos
        /// </summary>
        public int GetNumberOfPhotos()
        {
            return sendCommand<int>(new CommandArgs()
            {
                CommandId = 4,
            });
    
        }

        /// <summary>
        /// Gets logs
        /// </summary>
        public LogEntry[] GetLogs()
        {
            return sendCommand<LogEntry[]>(new CommandArgs()
            {
                CommandId = 2,
            });

        }

        /// <summary>
        /// Gets the settings
        /// </summary>
        public Settings GetSettings()
        {
            return sendCommand<Settings>(new CommandArgs()
            {
                CommandId = 1,
            });

        }

        /// <summary>
        /// Removes a handler, and return the updated settings
        /// </summary>
        /// <param name="handler">The handler to remove</param>
        public Settings RemoveHandler(string handler)
        {
            return sendCommand<Settings>(new CommandArgs()
            {
                CommandId = 3,
                Arg = handler
            });
        }

        /// <summary>
        /// Sends a command to service and returns result
        /// </summary>
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
        /// returns true if the GUIclient is connected, false otherwise.
        /// </summary>
        public bool Connected() { return client.Connected;  }

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
