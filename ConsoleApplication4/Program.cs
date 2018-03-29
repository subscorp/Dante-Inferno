using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hithalnou");

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
