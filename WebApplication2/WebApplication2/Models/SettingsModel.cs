using Communication;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class SettingsModel : WebModel
    {
        private Settings settings;

        public Settings Settings
        {
            get { return settings; }
            set { settings = value; }
        }

        public SettingsModel() : base()
        {
            settings = new Settings();
            settings.Handlers = new ObservableCollection<string>();
            GetSettings();
        }

        public void GetSettings()
        {
            if (client.Connected())
                settings = client.GetSettings();
        }

        public void Remove(string toErase)
        {
            Settings.Handlers.Remove(toErase);
            client.RemoveHandler(toErase);
        }
    }
}