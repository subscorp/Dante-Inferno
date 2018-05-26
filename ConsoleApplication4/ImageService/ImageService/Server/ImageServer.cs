using ImageService.Controller;
using ImageService.Controller.Handlers;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Logging.Modal;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ImageService.Server
{
    /// <summary>
    /// Class ImageServer.
    /// </summary>
    public class ImageServer
    {
        private Dictionary<string, IDirectoryHandler> handlersDict = new Dictionary<string, IDirectoryHandler>();
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageServer"/> class.
        /// </summary>
        /// <param name="handlers">The handlers.</param>
        /// <param name="ils">The ils.</param>
        /// <param name="iic">The iic.</param>
        public ImageServer(ObservableCollection<string> handlers, ILoggingService ils, IImageController iic)
        {
            m_logging = ils;
            m_controller = iic;

            foreach (string s in handlers)
            {

                IDirectoryHandler h = new DirectoryHandler(s, m_controller, m_logging);
                handlersDict.Add(s, h);
                CommandReceived += h.OnCommandReceived;
                h.DirectoryClose += CloseHandler;
            }

            handlers.CollectionChanged += (a, b) =>
            {
                if (b.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                {
                    var obj = b.OldItems.Cast<string>().ToArray();
                    handlersDict[obj[0]].OnCommandReceived(handlersDict[obj[0]], new CommandReceivedEventArgs(1, new string[0], ""));
                }
            };
        }

        /// <summary>
        /// Closes the server.
        /// </summary>
        public void CloseServer()
        {
            // commandID 1 means - close all handlers, so no particular path is required
            CommandReceived?.Invoke(this, new CommandReceivedEventArgs(1, null, null));
        }

        /// <summary>
        /// Closes the handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="d">The <see cref="DirectoryCloseEventArgs"/> instance containing the close command.</param>
        public void CloseHandler(object sender, DirectoryCloseEventArgs d)
        {
            IDirectoryHandler idh = (IDirectoryHandler)sender;
            CommandReceived -= idh.OnCommandReceived;
            idh.DirectoryClose -= CloseHandler;
            handlersDict.Remove(d.DirectoryPath);
        }

        #region Members
        private IImageController m_controller;
        private ILoggingService m_logging;
        #endregion

        #region Properties
        // The event that notifies the handlers about a new Command being Received
        public event EventHandler<CommandReceivedEventArgs> CommandReceived;
        #endregion


    }
}
