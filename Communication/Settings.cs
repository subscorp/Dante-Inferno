using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace Communication
{
    /// <summary>
    /// Class for Settings of image service
    /// </summary>
    public class Settings
        
    {
        /// <summary>
        /// Gets or sets the log source.
        /// </summary>
        /// <value>The log source.</value>
        public string LogSource { get; set; }
        /// <summary>
        /// Gets or sets the name of the log.
        /// </summary>
        /// <value>The name of the log.</value>
        public string LogName { get; set; }
        /// <summary>
        /// Gets or sets the directory handlers.
        /// </summary>
        /// <value>The handlers.</value>
        public ObservableCollection<string> Handlers { get; set; }
        /// <summary>
        /// Gets or sets the output directory to which images are transferred.
        /// </summary>
        /// <value>The output dir.</value>
        public string OutputDir { get; set; }
        /// <summary>
        /// Gets or sets the size of the thumbnail.
        /// </summary>
        /// <value>The size of the thumbnail.</value>
        public string ThumbnailSize { get; set; }

        /// <summary>
        /// Constructor - empty
        /// </summary>
        public Settings()
        {
        }

        /// <summary>
        /// Serializes Settings into a string for sending to client
        /// </summary>
        /// <returns>System.String.</returns>
        public string ToJSON()
        {
            JObject appConfigObj = new JObject();
            appConfigObj["LogSource"] = LogSource;
            appConfigObj["LogName"] = LogName;
            appConfigObj["OutputDir"] = OutputDir;
            appConfigObj["ThumbnailSize"] = ThumbnailSize;
            JArray handlers = new JArray();
            foreach (string handler in Handlers)
            {
                handlers.Add(handler);
            }
            appConfigObj["Handlers"] = handlers;
            return appConfigObj.ToString();
        }

    }
}
