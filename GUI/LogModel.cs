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
    internal class LogModel : IModel
    {
        //private ConsoleClient client;
        private Dictionary<string, string> TypeToColor;

        public ObservableCollection<LogEntry> Logs
        {
            get;
            set;
        }

        //public async Task GetLogs()
        //{
        //    System.Timers.Timer t = new System.Timers.Timer(10 * 1000);
        //    t.Interval = 5000;
        //    t.Elapsed += async (a, b) =>
        //    {
        //        var logs = await _guiClient.GetLogs();
        //        Logs.Clear();

        //        foreach (var logEntry in logs)
        //        {
        //            logEntry.Color = TypeToColor[logEntry.Type];
        //            Logs.Add(logEntry);
        //        }

        //        ClearLogs();
        //    };
        //    t.Enabled = true;
        //}

        public void ClearLogs()
        {
            for (int i = Logs.Count - 1; i > 0; i--)
            {
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

        public LogModel(): base()
        {
            TypeToColor = new Dictionary<string, string>();
            TypeToColor.Add("INFO", "Green");
            TypeToColor.Add("WARNING", "Yellow");
            TypeToColor.Add("ERROR", "Red");

            Logs = new ObservableCollection<LogEntry>();
            BindingOperations.EnableCollectionSynchronization(Logs, Logs);

            new Task(() =>
            {
                Console.WriteLine("im alive");
                System.Timers.Timer t = new System.Timers.Timer(10 * 1000);
                t.Interval = 5000;
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