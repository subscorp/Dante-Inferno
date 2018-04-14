using ImageService.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    /// <summary>
    /// Interface for commands
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Executes the command with specified arguments.
        /// </summary>
        /// <param name="args">The arguments - depending on the command.</param>
        /// <param name="result">if set to <c>true</c> return a success message.</param>
        /// <returns> A message of success or failure </returns>
        string Execute(string[] args, out bool result);
    }
}
