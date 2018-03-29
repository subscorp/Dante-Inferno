
using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging
{
    public class LoggingService : ILoggingService
    {
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        public void Log(string message, MessageTypeEnum type)
        {
            MessageReceivedEventArgs mrea = new MessageReceivedEventArgs();
            mrea.Message = message;
            mrea.Status = type;
            MessageReceived.Invoke(this, mrea);
        }
    }
}
