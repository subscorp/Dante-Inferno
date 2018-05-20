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
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new ImgService() 
            };
            ServiceBase.Run(ServicesToRun);   
        }
    }
}
