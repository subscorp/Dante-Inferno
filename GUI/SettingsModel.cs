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
        public string[] handlers
        {
            get;
            set;
        }
        public SettingsModel()
        {
            //TODO create connection, get the parameters and use them.
            settings = new ObservableCollection<string>()
                                        { "12", "path", "output", "aher" };
            handlers = new string[] { "dever", "herev", "haya" };
        }
    }
}