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
using ImageService.Logging;
using ImageService.Logging.Modal;

namespace ImageService.ImageService.ImageService.Server
{
    class AppConfigHandlerV2 : IClientHandler
    {
        public ILoggingService Ils { get; }
        private Settings settings;
        public AppConfigHandlerV2(Settings settings, ILoggingService ils)
        {
            Ils = ils;
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
                            Ils.Log("Error when deserializing or getting string!", MessageTypeEnum.FAIL);
                            return;
                        }


                        if (cmd.CommandId == 1)
                        {
                            Ils.Log("sending settings to the client:", MessageTypeEnum.INFO);
                            writer.Write(settings.ToJSON());
                        }
                        else if (cmd.CommandId == 2)
                        {
                            Ils.Log("sending log to the client\n", MessageTypeEnum.INFO);
                            writer.Write(JsonConvert.SerializeObject(Ils.Entries.Reverse().ToArray(), Formatting.Indented));
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
