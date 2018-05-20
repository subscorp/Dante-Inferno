using System.Collections.ObjectModel;

namespace GUI
{
    internal class SettingViewModel : ViewModel
    {
        
        private SettingsModel sm;

        public ObservableCollection<string> Settings
        {
            get => sm.settings;
            set
            {
                NotifyPropertyChanged("Settings");
            }
        }

        public ObservableCollection<string> Handlers
        {
            get => sm.handlers;
            set
            {
                NotifyPropertyChanged("Handlers");
            }
        }

        public void RemoveHandler()
        {
        }

        public SettingViewModel()
        {
            sm = new SettingsModel();
        }
        
    }
}