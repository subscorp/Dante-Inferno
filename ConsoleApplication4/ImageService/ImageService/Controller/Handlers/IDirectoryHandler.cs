using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Controller.Handlers
{
    /// <summary>
    /// Interface for handlers
    /// </summary>
    public interface IDirectoryHandler
    {
        // The Event That Notifies that the Directory is being closed
        event EventHandler<DirectoryCloseEventArgs> DirectoryClose;  
        
        /// <summary>
        /// Starts to handle the directory.
        /// </summary>
        /// <param name="dirPath">The dir path.</param>
        void StartHandleDirectory(string dirPath);             // The Function Receives the directory to Handle

        /// <summary>
        /// Handles the <see cref="E:CommandReceived" /> event.
        /// </summary>
        /// <param name="sender">The sender of the command.</param>
        /// <param name="e">The <see cref="CommandReceivedEventArgs"/> instance containing the command data.</param>

        // The Event that will be activated upon new Command
        void OnCommandReceived(object sender, CommandReceivedEventArgs e);     
    }
}
