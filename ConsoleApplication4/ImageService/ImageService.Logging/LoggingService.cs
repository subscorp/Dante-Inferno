﻿
using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Communication;

namespace ImageService.Logging
{
    /// <summary>
    /// Class LoggingService.
    /// </summary>
    /// <seealso cref="ImageService.Logging.ILoggingService" />
    public class LoggingService : ILoggingService
    {
        private readonly List<LogEntry> _entries = new List<LogEntry>();

        public IEnumerable<LogEntry> Entries => _entries;

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
            
            // The server will create a new LogEntry object when it sends info to the client
            // No, sorry, you get it form the EventLog... not from here. I got confused.
            MessageReceivedEventArgs mrea = new MessageReceivedEventArgs();
            mrea.Message = message;
            mrea.Status = type;
            this._entries.Add(new LogEntry()
            {
                Message = message,
                Type = type.ToString(),
                Time = DateTime.Now
            });
            MessageReceived?.Invoke(this, mrea);
        }
    }
}
