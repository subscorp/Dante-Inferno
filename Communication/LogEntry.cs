using System;

namespace Communication
{

    /// <summary>
    /// Class LogEntry for log messages
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public string Type { get; set; }
        /// <summary>
        /// Gets or sets the date of logging.
        /// </summary>
        /// <value>The time.</value>
        public DateTime? Time { get; set; }
        /// <summary>
        /// Gets or sets the background color.
        /// </summary>
        /// <value>The color.</value>
        public string Color { get; set; }
    }
}
