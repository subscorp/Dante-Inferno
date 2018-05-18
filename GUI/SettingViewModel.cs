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


        public SettingViewModel()
        {
            sm = new SettingsModel();
        }
    }
}