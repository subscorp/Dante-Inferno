using ImageService.Commands;
using ImageService.Infrastructure;
using ImageService.Infrastructure.Enums;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Controller
{
    /// <summary>
    /// Class for Controller of image service.
    /// </summary>
    /// <seealso cref="ImageService.Controller.IImageController" />
    public class ImageController : IImageController
    {
        private IImageServiceModal m_modal;                      // The Modal Object
        private Dictionary<int, ICommand> commands;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageController"/> class.
        /// </summary>
        /// <param name="modal">The modal which manages the output directory.</param>
        public ImageController(IImageServiceModal modal)
        {
            m_modal = modal;
            commands = new Dictionary<int, ICommand>()
            {
                {(int)CommandEnum.NewFileCommand, new NewFileCommand(m_modal) }
				// For Now will contain NEW_FILE_COMMAND, since CLOSE_COMMAND is activated by server
            };
        }

        /// <summary>
        /// Executes a command.
        /// </summary>
        /// <param name="commandID">The command identifier.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="resultSuccessful">if set to <c>true</c> [result successful].</param>
        /// <returns>Success or error message </returns>
        public string ExecuteCommand(int commandID, string[] args, out bool resultSuccessful)
        {
            return commands[commandID].Execute(args,out resultSuccessful);
        }
    }
}
