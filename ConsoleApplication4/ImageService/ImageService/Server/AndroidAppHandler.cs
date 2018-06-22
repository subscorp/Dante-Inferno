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
                            int finished;
                            //getting the name of the image
                            byte[] nameLength = reader.ReadBytes(4);
                            int nameLen = convertBytes(nameLength);
                            byte[] nameFromBytes = reader.ReadBytes(nameLen);
                            var imgName = System.Text.Encoding.Default.GetString(nameFromBytes);
                            
                            //getting the image
                            byte[] bytes = reader.ReadBytes(4);
                            int bin = convertBytes(bytes);
                            byte[] json = reader.ReadBytes(bin);
                            MemoryStream ms = new MemoryStream(json);

                            //saving the image
                            Image img = Image.FromStream(ms,true,true);
                            Bitmap returnImage = new Bitmap(img, img.Width, img.Height);
                            returnImage.Save(settings.Handlers[0] + "\\" + imgName);

                            //checking if the client completed the transfer
                            finished = reader.ReadByte();
                            if(finished == 1)
                            {
                                client.Close();
                                break;
                            }
                        }   
                        catch (Exception ex)
                        {
                            return;
                        } 
                    }

                }
            }).Start();
        }

        int convertBytes(byte[] bytes)
        {
            //convert binary number in byte array to int
            int multiplier = 1;
            int exponent = 8;
            int bin = 0;
            for (int j = 3; j >= 0; --j)
            {
                bin += (multiplier * bytes[j]);
                multiplier = (int)Math.Pow(2, exponent);
                exponent += 8;
            }
            return bin;
        }
    }
}
