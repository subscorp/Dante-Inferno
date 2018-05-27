using Communication;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GUI
{
    internal class SettingsModel : IModel
    {
        private string outputDir;
        private string logSource;
        private string logName;
        private string thumbnailSize;

        public ObservableCollection<string> Handlers
        {
            get;
            private set;
        }

        public string OutputDir
        {
            get { return outputDir; }
            set { outputDir = value; NotifyPropertyChanged("OutputDir"); }
        }

        public string LogSource
        {
            get { return logSource; }
            set { logSource = value; NotifyPropertyChanged("LogSource"); }
        }

        public string LogName
        {
            get { return logName; }
            set { logName = value; NotifyPropertyChanged("LogName"); }
        }

        public string ThumbnailSize
        {
            get { return thumbnailSize; }
            set { thumbnailSize = value; NotifyPropertyChanged("ThumbnailSize"); }
        }

        public SettingsModel() : base()
        {
            Handlers = new ObservableCollection<string>();
            BindingOperations.EnableCollectionSynchronization(Handlers, Handlers);

            Handlers.CollectionChanged += async (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (var handler in args.OldItems.Cast<string>())
                    {
                        await _guiClient.RemoveHandler(handler);
                        Thread.Sleep(3000);
                    }
                }
            };

            new Task(() =>
            {
                Console.WriteLine("im alive and kicking baby");
                System.Timers.Timer t = new System.Timers.Timer(5 * 1000);
                t.Elapsed += async (a, b) =>
                {
                    var settings = await _guiClient.GetSettings();

                    OutputDir = settings.OutputDir;
                    LogSource = settings.LogSource;
                    LogName = settings.LogName;
                    ThumbnailSize = settings.ThumbnailSize;
                    
                    Handlers.Clear();
                    foreach (var handler in settings.Handlers)
                    {
                        Handlers.Add(handler);
                    }
                };
                t.Enabled = true;
            }).Start();

        }

    }
}