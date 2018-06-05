using Communication;
using System;
using System.Collections.Generic;
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
            settings = client.GetSettings();
        }
    }
}