using ImageService.Infrastructure;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    /// <summary>
    /// Class for NewFile Command
    /// </summary>
    /// <seealso cref="ImageService.Commands.ICommand" />
    public class NewFileCommand : ICommand
    {
        private IImageServiceModal m_modal;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewFileCommand"/> class.
        /// </summary>
        /// <param name="modal">The modal which executes the command.</param>
        public NewFileCommand(IImageServiceModal modal)
        {
            m_modal = modal;            // Storing the Modal
        }

        /// <summary>
        /// Executes the command - tells the modal to add a new file.
        /// </summary>
        /// <param name="args">The file's name.</param>
        /// <param name="result">if set to <c>true</c> the file was added Successfully.</param>
        /// <returns>the new path, or an error message</returns>
        public string Execute(string[] args, out bool result)
        {
            return m_modal.AddFile(args[0], out result);
            // The String Will Return the New Path if result = true, 
            // and the error message else wise.
        }
    }
}
