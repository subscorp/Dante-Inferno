using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging
{
    /// <summary>
    /// Interface for logging service
    /// </summary>
    public interface ILoggingService
    {
        /// <summary>
        /// Occurs when a message is received.
        /// </summary>
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        void Log(string message, MessageTypeEnum type);           // Logging the Message
    }
}
