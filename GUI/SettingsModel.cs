using System.Collections.ObjectModel;

namespace GUI
{
    internal class SettingsModel
    {
        //private mashehou shekashour leTCP

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
        public SettingsModel()
        {
            //TODO create connection, get the parameters and use them.
            settings = new ObservableCollection<string>()
                                        { "output", "source", "log", "thumb" };
            handlers = new ObservableCollection<string>()
                                        { "dever", "herev", "haya" };
        }
    }
}