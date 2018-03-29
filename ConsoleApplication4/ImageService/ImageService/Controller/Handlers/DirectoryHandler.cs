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
    public class DirectoryHandler : IDirectoryHandler
    {
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

        public event EventHandler<DirectoryCloseEventArgs> DirectoryClose;              // The Event That Notifies that the Directory is being closed

        public void OnCommandReceived(object sender, CommandReceivedEventArgs e)
        {
            // if the command is to close all handlers
            if(e.CommandID == 1 || (e.CommandID == 2 && e.RequestDirPath == m_path))
            {
                DirectoryClose.Invoke(this, new DirectoryCloseEventArgs(m_path, "closed handler"));
                //TODO close dirWatcher, i guess by using dispose...
                m_logging.Log("closing handler for " + m_path, MessageTypeEnum.INFO);
            } else
            {
                //TODO execute the commands, and then update the m_logging by Log method
            }

        }

        public void StartHandleDirectory(string dirPath)
        {
            //throw new NotImplementedException();
        }

        // Implement Here!
    }
}
