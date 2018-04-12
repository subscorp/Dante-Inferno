using ImageService.Modal;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Infrastructure;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Logging.Modal;
using System.Text.RegularExpressions;

namespace ImageService.Controller.Handlers
{
    /// <summary>
    /// Class for Directory Handler.
    /// </summary>
    /// <seealso cref="ImageService.Controller.Handlers.IDirectoryHandler" />
    public class DirectoryHandler : IDirectoryHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryHandler"/> class.
        /// </summary>
        /// <param name="directory">The directory path.</param>
        /// <param name="controller">The controller, used for commanding the output directory.</param>
        /// <param name="ils">The loggings service, which get messages from handler to the service.</param>
        public DirectoryHandler(string directory, IImageController controller, ILoggingService ils)
        {
            m_path = directory;
            m_logging = ils;
            StartHandleDirectory(m_path);
            m_controller = controller;
        }

        #region Members
        private IImageController m_controller;              // The Image Processing Controller
        private ILoggingService m_logging;
        private FileSystemWatcher m_dirWatcher;             // The Watcher of the Dir
        private string m_path;                              // The Path of directory
        #endregion

        // The Event That Notifies the server that the handler is being closed
        public event EventHandler<DirectoryCloseEventArgs> DirectoryClose;              

        /// <summary>
        /// Handles the <see cref="E:CommandReceived" /> event.
        /// </summary>
        /// <param name="sender">The object which gave the command (the server).</param>
        /// <param name="e">The <see cref="CommandReceivedEventArgs"/> instance containing the event data.</param>
        public void OnCommandReceived(object sender, CommandReceivedEventArgs e)
        {
            // if the command is to close all handlers, or this particular one
            if(e.CommandID == 1 || (e.CommandID == 2 && e.RequestDirPath == m_path))
            {
                DirectoryClose.Invoke(this, new DirectoryCloseEventArgs(m_path, "closed handler"));

                m_dirWatcher.Dispose();
                m_logging.Log("closing handler for " + m_path, MessageTypeEnum.INFO);
            } else
            {
                //empty for now, since only close commands can come from server
            }

        }

        /// <summary>
        /// Creates the file in output directory.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="FileSystemEventArgs"/> instance containing the name and path of file.</param>
        public void CreateFile(object sender, FileSystemEventArgs e)
        {
            bool result;
            string[] args = {e.FullPath, e.Name};
            m_controller.ExecuteCommand(0, args, out result);
            if (result)
            {
                m_logging.Log("Successfully added " + e.Name + "to folder", MessageTypeEnum.INFO);
            }
            else m_logging.Log("Couldn't add " + e.Name + "to folder", MessageTypeEnum.FAIL);
        }

        /// <summary>
        /// Starts to watch the directory.
        /// </summary>
        /// <param name="dirPath">The directory path.</param>
        public void StartHandleDirectory(string dirPath)
        {
            m_dirWatcher = new FileSystemWatcher();
            m_dirWatcher.BeginInit();
            m_dirWatcher.Path = dirPath;
            m_dirWatcher.EnableRaisingEvents = true;
            m_dirWatcher.Created += CreateFile;
            m_dirWatcher.EndInit();
        }
        
    }
}
