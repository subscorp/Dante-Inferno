using Communication;
using System;
using System.Collections.ObjectModel;


namespace GUI
{
    internal class SettingsModel : IModel
    {
        private ConsoleClient client;

        public ObservableCollection<string> settings
        {
            get;
            set;
        }
        public ObservableCollection<string> handlers
        {
            get;
            set;
        }

        //public Settings mySettings
        //{
        //    get => Settings.FromJSON(Client.settingstr);
        //    set;
        //}


        public SettingsModel()
        {
            client = ConsoleClient.Instance;
            client.HandleClient();

            Settings m_settings = client.Settings;

            settings = new ObservableCollection<string>()
            { m_settings.OutputDir, m_settings.LogSource, m_settings.LogName, m_settings.ThumbnailSize };

            Console.WriteLine("Handlers:");

            handlers = m_settings.Handlers;
        }

    }
}