using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Controller
{
    /// <summary>
    /// Interface IImageController
    /// </summary>
    public interface IImageController
    {
        /// <summary>
        /// Executes a command.
        /// </summary>
        /// <param name="commandID">The command identifier.</param>
        /// <param name="args">The arguments for executing the command.</param>
        /// <param name="result">if set to <c>true</c> [result].</param>
        /// <returns>A success or error message</returns>
        string ExecuteCommand(int commandID, string[] args, out bool result);          // Executing the Command Request
    }
}
