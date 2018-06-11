using Communication;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    /// <summary>
    /// Class for model, which manages Configuration web page.
    /// </summary>
    public class SettingsModel : WebModel
    {
        private Settings settings;

        public Settings Settings
        {
            get { return settings; }
            set { settings = value; }
        }

        //Constructor
        public SettingsModel() : base()
        {
            settings = new Settings();
            settings.Handlers = new ObservableCollection<string>();
            GetSettings();
        }

        // Gets settings from the service
        public void GetSettings()
        {
            if (client.Connected())
                settings = client.GetSettings();
        }

        // Removes a handler from list and sends command to service
        public void Remove(string toErase)
        {
            Settings.Handlers.Remove(toErase);
            client.RemoveHandler(toErase);
        }
    }
}