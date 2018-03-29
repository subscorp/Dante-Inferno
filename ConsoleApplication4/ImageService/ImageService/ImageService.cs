using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ImageService.Server;
using ImageService.Controller;
using ImageService.Modal;
using ImageService.Logging;
using ImageService.Logging.Modal;
using System.Configuration;
using ImageService.Infrastructure;

namespace ImageService
{
    public enum ServiceState
    {
        SERVICE_STOPPED = 0x00000001,
        SERVICE_START_PENDING = 0x00000002,
        SERVICE_STOP_PENDING = 0x00000003,
        SERVICE_RUNNING = 0x00000004,
        SERVICE_CONTINUE_PENDING = 0x00000005,
        SERVICE_PAUSE_PENDING = 0x00000006,
        SERVICE_PAUSED = 0x00000007,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ServiceStatus
    {
        public int dwServiceType;
        public ServiceState dwCurrentState;
        public int dwControlsAccepted;
        public int dwWin32ExitCode;
        public int dwServiceSpecificExitCode;
        public int dwCheckPoint;
        public int dwWaitHint;
    };

    public partial class ImageService : ServiceBase
    {

        private ImageServer m_imageServer;          // The Image Server
		private IImageServiceModal modal;
		private IImageController controller;
        private EventLog eventLog1;
        private ILoggingService logging;

        public ImageService()
        {
            InitializeComponent();
        }

        // Here You will Use the App Config!
        protected override void OnStart(string[] args)
        {
            Console.WriteLine("notsar");
            string logSource = ConfigurationManager.AppSettings["SourceName"];
            string logName = ConfigurationManager.AppSettings["LogName"];
            string[] handlers = ConfigurationManager.AppSettings["Handler"].Split(';');

            this.eventLog1 = new EventLog();
            //initialize the EventLogger with values from configuration file
            ((ISupportInitialize)(eventLog1)).BeginInit();
            // 
            if (!EventLog.SourceExists(logSource))
            {
                EventLog.CreateEventSource(logSource, logName);
            }
            eventLog1.Source = logSource;
            eventLog1.Log = logName;

            ((ISupportInitialize)(eventLog1)).EndInit();

            //logging.MessageReceived += onMsg;

            m_imageServer = new ImageServer(handlers);

            eventLog1.WriteEntry("ImageService started");

        }
        
        protected void onMsg(object sender, MessageReceivedEventArgs e)
        {
            eventLog1.WriteEntry(e.Message);        
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("ImageService stopped.");
        }

        private void InitializeComponent()
        {
            // 
            // ImageService
            // 
            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = true;
            this.ServiceName = "ImageService";

        }


    }
}
