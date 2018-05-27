using Communication;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Threading;

namespace GUI
{

    /// <summary>
    /// A model for managing log information.
    /// </summary>
    internal class LogModel : IModel
    {
        //private ConsoleClient client;
        private Dictionary<string, string> TypeToColor;

        /// <summary>
        /// Gets or sets the logs' list.
        /// </summary>
        /// <value>The logs.</value>
        public ObservableCollection<LogEntry> Logs
        {
            get;
            set;
        }


        /// <summary>
        /// Clears the logs from before current service start.
        /// </summary>
        public void ClearLogs()
        {
            for (int i = Logs.Count - 1; i > 0; i--)
            {
                //if there's a message from before current start of service, erase it. 
                if (Logs[i].Message == "ImageService stopped.")
                {
                    do
                    {
                        Logs.RemoveAt(i);
                        i--;
                    } while (i > -1);
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogModel"/> class.
        /// </summary>
        public LogModel(): base()
        {
            //Initializes type to color dictionary for background colors
            TypeToColor = new Dictionary<string, string>();
            TypeToColor.Add("INFO", "Green");
            TypeToColor.Add("WARNING", "Yellow");
            TypeToColor.Add("ERROR", "Red");

            Logs = new ObservableCollection<LogEntry>();
            BindingOperations.EnableCollectionSynchronization(Logs, Logs);

            //recheck for log update every 5 seconds
            new Task(() =>
            {
                System.Timers.Timer t = new System.Timers.Timer(5000);
                t.Elapsed += async (a, b) =>
                {
                    var logs = await _guiClient.GetLogs();
                    Logs.Clear();

                    foreach (var logEntry in logs)
                    {
                        logEntry.Color = TypeToColor[logEntry.Type];
                        Logs.Add(logEntry);
                    }

                    ClearLogs();
                };
                t.Enabled = true;
            }).Start();


        }
    }
}