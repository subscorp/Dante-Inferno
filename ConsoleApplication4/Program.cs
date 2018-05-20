using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Controller;
using ImageService.ImageService.ImageService.Server;

namespace ImageService
{
    class Program
    {
        /// <summary>
        /// activates the ImageService.
        /// </summary>
        /// <param name="args">The arguments (null in this case).</param>
        static void Main(string[] args)
        {
            //ImageService.ImageService.Server.IClientHandler ch = new ImageService.ImageService.Server.StudentHandler();
            ImageService.ImageService.Server.IClientHandler ch = new ImageService.ImageService.Server.AppConfigHandlerV2();
            //IClientHandler ch = new MultiplyByTwoHandler();
            ImageService.ImageService.Server.Server server = new ImageService.ImageService.Server.Server(8000, ch);
            server.Start();

         //      tcp myTcp = new tcp()
         //      myTcp.Run();
            /*
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new ImageService() 
            };
            ServiceBase.Run(ServicesToRun);
            */
        }

    }
}
