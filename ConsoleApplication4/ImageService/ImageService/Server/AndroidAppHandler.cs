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
using System.Drawing;

namespace ImageService.ImageService.ImageService.Server
{
    class AndroidAppHandler : IClientHandler
    {
        public ILoggingService Ils { get; }
        private Settings settings;
        public AndroidAppHandler(Settings settings, ILoggingService ils)
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
                    try
                    {

                            
                            //getting pictures from the client
                            int numBytes = reader.ReadInt32();
                            //byte[] json = reader.ReadBytes(800 * 1000);
                            byte[] json = reader.ReadBytes(numBytes);
                            Console.WriteLine("read {0} bytes", json.Length);
                            MemoryStream ms = new MemoryStream(json);

                            Console.WriteLine("before Image img");
                            Image img = Image.FromStream(ms,true,true);
                            Console.WriteLine("after Image img");

                            Bitmap returnImage = new Bitmap(img, img.Width, img.Height);
                            //   img.Tag = "myImage.jpg";
                            //   string imgName = img.Tag.ToString();
                            string imgName = "myImage.jpg";
                            Console.WriteLine("trying to save the photo");
                            returnImage.Save(@"C:\ImageFolders\folder1\" + imgName);
                            Console.WriteLine("saved the image");
                            //cmd = JsonConvert.DeserializeObject<CommandArgs>(json); 
                        }   
                        catch (Exception ex)
                        {
                            return;
                        } 
                    }

                }
            }).Start();
        }
    }
}
