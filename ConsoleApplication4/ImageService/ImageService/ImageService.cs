﻿using System;
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

    /// <summary>
    /// Class ImageService.
    /// </summary>
    /// <seealso cref="System.ServiceProcess.ServiceBase" />
    public partial class ImageService : ServiceBase
    {

        private ImageServer m_imageServer;          // The Image Server
		private IImageServiceModal modal;
		private IImageController controller;
        private EventLog eventLog1;
        private ILoggingService logging;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageService"/> class.
        /// </summary>
        public ImageService()
        {
            InitializeComponent();
        }


        protected override void OnStart(string[] args)
        {
            logging = new LoggingService();

            string logSource = ConfigurationManager.AppSettings["SourceName"];
            string logName = ConfigurationManager.AppSettings["LogName"];
            string[] handlers = ConfigurationManager.AppSettings["Handler"].Split(';');
            string outputDir = ConfigurationManager.AppSettings["OutputDir"];
            string thumbnailSize = ConfigurationManager.AppSettings["ThumbnailSize"];

            eventLog1 = new EventLog();

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

            logging.MessageReceived += OnMsg;
            
            modal = new ImageServiceModal(outputDir, int.Parse(thumbnailSize));
            controller = new ImageController(modal);

            m_imageServer = new ImageServer(handlers, logging, controller);

            eventLog1.WriteEntry("ImageService started");

        }
        
        protected void OnMsg(object sender, MessageReceivedEventArgs e)
        {
            eventLog1.WriteEntry(e.Message);        
        }

        protected override void OnStop()
        {
            m_imageServer.CloseServer();
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
