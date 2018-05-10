using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Configuration;

namespace ImageService.ImageService.ImageService.Server
{
    class AppConfigHandler : IClientHandler
    {
        public void HandleClient(TcpClient client)
        {
           
            string logSource = ConfigurationManager.AppSettings["SourceName"];
            string logName = ConfigurationManager.AppSettings["LogName"];
            string[] handlers = ConfigurationManager.AppSettings["Handler"].Split(';');
            string outputDir = ConfigurationManager.AppSettings["OutputDir"];
            string thumbnailSize = ConfigurationManager.AppSettings["ThumbnailSize"];

            new Task(() =>
            {
                using (NetworkStream stream = client.GetStream())
                using (BinaryReader reader = new BinaryReader(stream))
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    while (true)
                    {
                        Console.WriteLine("Sending AppConfigObject");
                        string studentStr = reader.ReadString();
                        Console.WriteLine("got studentStr: {0}", studentStr);
               //         student = Student.FromJSON(studentStr);
               //         if (student.Name.Equals("exit"))
               //             break;
               //         studentGrade = student.Grade;
               //         if (studentGrade >= 60)
               //         {
               //             Console.WriteLine("notifying the client that the student passed");
               //             writer.Write(1);
               //         }
               //         else
               //         {
               //             Console.WriteLine("notifying the client that the student failed");
               //             writer.Write(0);
               //         }
                    }

                }

                /*
                string commandLine = reader.ReadLine();
                Console.WriteLine("Got command: {0}", commandLine);
                string result = ExecuteCommand(commandLine, client);
                writer.Write(result);
                */
                client.Close();
                Console.WriteLine("client disconnected");
            }).Start();
        }

        private string ExecuteCommand(string commandLine, TcpClient client)
        {
            throw new NotImplementedException();
        }
    }
}
