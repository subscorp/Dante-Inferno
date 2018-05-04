using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Controller;

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
            tcp myTcp = new tcp();
            myTcp.Run();
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
