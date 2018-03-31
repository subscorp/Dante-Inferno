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
            Console.WriteLine("hithalnou");
            /*
            bool result;
            string[] stringArr = { "C:\\ImageFolders\\folder1\\butterfly.jpg" };
            IImageController controller = new ImageController(new ImageServiceModal("C:\\ImageFolders\\output"));
            controller.ExecuteCommand(0, stringArr , out result);
            return;
            */
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new ImageService() 
            };
            ServiceBase.Run(ServicesToRun);
            
            Console.WriteLine("siyamnou ");
        }

    }
}
