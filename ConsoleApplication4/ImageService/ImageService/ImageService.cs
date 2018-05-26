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
using Communication;

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

    /// <summary>
    /// Class ImageService.
    /// </summary>
    /// <seealso cref="System.ServiceProcess.ServiceBase" />
    public partial class ImgService : ServiceBase
    {

        private ImageServer m_imageServer;          // The Image Server
		private IImageServiceModal modal;
		private IImageController controller;
        private ILoggingService logging;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageService"/> class.
        /// </summary>
        public ImgService()
        {
            InitializeComponent();
        }

        public void ForceRun()
        {
            this.OnStart(null);
        }
        
        /// <summary>
        /// executes when Start command is sent to the service by the SCM
        /// </summary>
        /// <param name="args">Data passed by the start command (null in our case).</param>
        protected override void OnStart(string[] args)
        {
            logging = new LoggingService();

            //Reads parameters from app.config
            string logSource = ConfigurationManager.AppSettings["SourceName"];
            string logName = ConfigurationManager.AppSettings["LogName"];
            string[] handlers = ConfigurationManager.AppSettings["Handler"].Split(';');
            string outputDir = ConfigurationManager.AppSettings["OutputDir"];
            string thumbnailSize = ConfigurationManager.AppSettings["ThumbnailSize"];

            // Creating a new Settings object with the info above
            Settings settings = new Settings()
            {
                Handlers = new System.Collections.ObjectModel.ObservableCollection<string>(handlers),
                LogName = logName,
                LogSource = logSource,
                OutputDir = outputDir,
                ThumbnailSize = thumbnailSize
            };
            LogContainer.CreateLog(logSource, logName);

            //adds the eventLog OnMsg method to the logging service event.
            logging.MessageReceived += OnMsg;


            //creates modal, controller and server
            modal = new ImageServiceModal(outputDir, int.Parse(thumbnailSize));
            controller = new ImageController(modal);
            m_imageServer = new ImageServer(settings.Handlers, logging, controller);

            ImageService.ImageService.Server.IClientHandler ch = new ImageService.ImageService.Server.AppConfigHandlerV2(settings);
            ImageService.ImageService.Server.Server server = new ImageService.ImageService.Server.Server(8000, ch);
            server.Start();

            LogContainer.Log.WriteEntry("ImageService started");
        }
        
        /// <summary>
        /// Writes a Message into Event Log.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The Message.</param>
        protected void OnMsg(object sender, MessageReceivedEventArgs e)
        {

            LogContainer.Log.WriteEntry(e.Message);        
        }

        /// <summary>
        /// Closes server, and notifies the EventLog that the service will close
        /// </summary>
        protected override void OnStop()
        {
            m_imageServer.CloseServer();
            LogContainer.Log.WriteEntry("ImageService stopped.");
        }

        /// <summary>
        /// Sets some parameters of the service before initialization
        /// </summary>
        private void InitializeComponent()
        {
            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = true;
            this.ServiceName = "ImageService";
        }
    }

    public static class LogContainer
    {
        public static EventLog Log { get; set; }
        public static void CreateLog(string logSource, string logName)
        {
            Log = new EventLog();

            //initialize the EventLogger with values from configuration file
            ((ISupportInitialize)(Log)).BeginInit();
            // 
            if (!EventLog.SourceExists(logSource))
            {
                EventLog.CreateEventSource(logSource, logName);
            }
            Log.Source = logSource;
            Log.Log = logName;

            ((ISupportInitialize)(Log)).EndInit();
        }
    }
}
