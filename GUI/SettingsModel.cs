using Communication;
using System.Collections.ObjectModel;


namespace GUI
{
    internal class SettingsModel : IModel
    {
        private ConsoleClient Client;

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
            Client = ConsoleClient.Instance;
            //Client.HandleClient();

            settings = new ObservableCollection<string>()
                                        { "output", "source", "log", "thumb" };
            handlers = new ObservableCollection<string>()
                                        { "dever", "herev", "haya" };
        }

    }
}