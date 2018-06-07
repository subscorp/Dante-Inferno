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
    class DesktopGUIHandler : IClientHandler
    {
        public ILoggingService Ils { get; }
        private Settings settings;
        public DesktopGUIHandler(Settings settings, ILoggingService ils)
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
                            //getting command from the client
                            var json = reader.ReadString();
                            cmd = JsonConvert.DeserializeObject<CommandArgs>(json);
                        }
                        catch (Exception ex)
                        {
                            return;
                        }

                        //settings command
                        if (cmd.CommandId == 1)
                        {
                            writer.Write(settings.ToJSON());
                        }

                        //log command
                        else if (cmd.CommandId == 2)
                        {
                            writer.Write(JsonConvert.SerializeObject(Ils.Entries.Reverse().ToArray(), Formatting.Indented));
                        }

                        //remove handler command
                        else if (cmd.CommandId == 3)
                        {
                            settings.Handlers.Remove(cmd.Arg);
                            writer.Write(settings.ToJSON());
                        }

                        //remove handler command
                        else if (cmd.CommandId == 4)
                        {
                            int fCount = Directory.GetFiles(settings.OutputDir, "*", SearchOption.AllDirectories).Length;
                            writer.Write(fCount.ToString());
                        }
                    }

                }
            }).Start();
        }
    }
}
