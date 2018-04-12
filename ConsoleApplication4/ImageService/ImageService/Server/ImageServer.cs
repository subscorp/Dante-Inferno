using ImageService.Controller;
using ImageService.Controller.Handlers;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Logging.Modal;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ImageService.Server
{
    public class ImageServer
    {
        public ImageServer(string[] handlers, ILoggingService ils, IImageController iic)
        {
            m_logging = ils;
            m_controller = iic;

            foreach(string s in handlers)
            {
                IDirectoryHandler h = new DirectoryHandler(s, m_controller, m_logging);
                CommandReceived += h.OnCommandReceived;
                h.DirectoryClose += CloseHandler;
            }
        }

        public void CloseServer()
        {
            // commandID 1 means - close all handlers, so no particular path is required
            CommandReceived.Invoke(this, new CommandReceivedEventArgs(1, null, null));
            m_logging.Log("closing all handlers", MessageTypeEnum.INFO);
        }

        public void CloseHandler(object sender, DirectoryCloseEventArgs d)
        {
            IDirectoryHandler idh = (IDirectoryHandler)sender;
            CommandReceived -= idh.OnCommandReceived;
            idh.DirectoryClose -= CloseHandler;
        }

        #region Members
        private IImageController m_controller;
        private ILoggingService m_logging;
        #endregion

        #region Properties
        public event EventHandler<CommandReceivedEventArgs> CommandReceived;          // The event that notifies about a new Command being Received
        #endregion

       
    }
}
