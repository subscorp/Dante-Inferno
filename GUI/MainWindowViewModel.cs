using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GUI
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {

        public MainWindowViewModel()
        {
            
        }

        public LogViewModel LogViewModel { get; set; } = new LogViewModel();

        public SettingViewModel SettingsViewModel { get; set; } = new SettingViewModel();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}