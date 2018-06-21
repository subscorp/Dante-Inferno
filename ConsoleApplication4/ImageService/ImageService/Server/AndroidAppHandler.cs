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
                            //getting the name of the image
                            byte[] nameLength = reader.ReadBytes(4);
                            int nameLen = convertBytes(nameLength);
                            byte[] nameFromBytes = reader.ReadBytes(nameLen);
                            var imgName = System.Text.Encoding.Default.GetString(nameFromBytes);
                            Console.WriteLine("nameToString is:" + imgName);
                            
                            //getting the image
                            byte[] bytes = reader.ReadBytes(4);
                            int bin = convertBytes(bytes);
                            Console.WriteLine("numBytes is {0}", bin);
                            byte[] json = reader.ReadBytes(bin);
                            Console.WriteLine("read {0} bytes", json.Length);
                            MemoryStream ms = new MemoryStream(json);

                            //saving the image
                            Console.WriteLine("before Image img");
                            Image img = Image.FromStream(ms,true,true);
                            Console.WriteLine("after Image img");
                            Bitmap returnImage = new Bitmap(img, img.Width, img.Height);
                            Console.WriteLine("trying to save the photo");
                            returnImage.Save(@"C:\ImageFolders\folder1\" + imgName);
                            Console.WriteLine("saved the image {0}",imgName);
                            Console.WriteLine();
                        }   
                        catch (Exception ex)
                        {
                            return;
                        } 
                    }
                    Console.WriteLine("got out of while loop");

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
