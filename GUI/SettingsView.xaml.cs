using System.Windows.Controls;

namespace GUI
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
            this.DataContext = new SettingViewModel();
        }
    }
}
