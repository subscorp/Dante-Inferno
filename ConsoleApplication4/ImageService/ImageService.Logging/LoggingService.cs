
using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging
{
    /// <summary>
    /// Class LoggingService.
    /// </summary>
    /// <seealso cref="ImageService.Logging.ILoggingService" />
    public class LoggingService : ILoggingService
    {
        /// <summary>
        /// Occurs when a message is received.
        /// </summary>
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        /// <summary>
        /// Logs the specified message, and invokes the events of it's writing.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        public void Log(string message, MessageTypeEnum type)
        {
            MessageReceivedEventArgs mrea = new MessageReceivedEventArgs();
            mrea.Message = message;
            mrea.Status = type;
            MessageReceived.Invoke(this, mrea);
        }
    }
}
