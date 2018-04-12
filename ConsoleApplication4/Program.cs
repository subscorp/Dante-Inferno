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
        static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new ImageService() 
            };
            ServiceBase.Run(ServicesToRun);
        }

    }
}
