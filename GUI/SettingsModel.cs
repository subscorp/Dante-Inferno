using Communication;
using System.Collections.ObjectModel;


namespace GUI
{
    internal class SettingsModel : IModel
    {
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
        {            //Client.HandleClient();

            settings = new ObservableCollection<string>()
                                        { };
            handlers = new ObservableCollection<string>()
                                        {  };
        }

    }
}