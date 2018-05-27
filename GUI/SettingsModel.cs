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
    /// <summary>
    /// A model for managing settings information.
    /// </summary>
    internal class SettingsModel : IModel
    {
        private string outputDir;
        private string logSource;
        private string logName;
        private string thumbnailSize;

        /// <summary>
        /// Gets the handlers.
        /// </summary>
        /// <value>The handlers.</value>
        public ObservableCollection<string> Handlers
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the output dir.
        /// </summary>
        /// <value>The output dir.</value>
        public string OutputDir
        {
            get { return outputDir; }
            set { outputDir = value; NotifyPropertyChanged("OutputDir"); }
        }

        /// <summary>
        /// Gets or sets the log source.
        /// </summary>
        /// <value>The log source.</value>
        public string LogSource
        {
            get { return logSource; }
            set { logSource = value; NotifyPropertyChanged("LogSource"); }
        }

        /// <summary>
        /// Gets or sets the name of the log.
        /// </summary>
        /// <value>The name of the log.</value>
        public string LogName
        {
            get { return logName; }
            set { logName = value; NotifyPropertyChanged("LogName"); }
        }

        /// <summary>
        /// Gets or sets the size of the thumbnail.
        /// </summary>
        /// <value>The size of the thumbnail.</value>
        public string ThumbnailSize
        {
            get { return thumbnailSize; }
            set { thumbnailSize = value; NotifyPropertyChanged("ThumbnailSize"); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsModel"/> class.
        /// </summary>
        public SettingsModel() : base()
        {
            Handlers = new ObservableCollection<string>();
            BindingOperations.EnableCollectionSynchronization(Handlers, Handlers);

            //Add to changes in handlers' list a function for removal of handler from server.
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

            //check for settings every 5 seconds
            new Task(() =>
            {
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