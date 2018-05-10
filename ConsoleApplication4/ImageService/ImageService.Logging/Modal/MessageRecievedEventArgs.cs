using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging.Modal
{
    /// <summary>
    /// Class MessageReceivedEventArgs, for message events
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class MessageReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the status of message - INFO, FAIL or WARNING.
        /// </summary>
        /// <value>The status.</value>
        public MessageTypeEnum Status { get; set; }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }
    }
}
