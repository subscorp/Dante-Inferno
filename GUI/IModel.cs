using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace GUI
{
    /// <summary>
    /// Class IModel - anstract class for models which deal with client-server connection and get/set data.
    /// </summary>
    internal abstract class IModel : INotifyPropertyChanged
    {
        protected GUIClient _guiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="IModel"/> class.
        /// </summary>
        public IModel()
        {
            var t = Connect();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="propName">Name of the property.</param>
        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        /// <summary>
        /// Initializes client-server connection. 
        /// </summary>
        /// <returns>Task.</returns>
        public async Task Connect()
        {
            _guiClient = GUIClient.Instance;
            await _guiClient.Connect();
        }


        }
    }