using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private GUIClient _guiClient;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindowViewModel MainWindowViewModel
        {
            get;
            set;
        } = new MainWindowViewModel();

        public async Task Query()
        {
            _guiClient = new GUIClient();
            await _guiClient.Connect();
            var settings = await _guiClient.GetSettings();
            var logs = await _guiClient.GetLogs();
            MainWindowViewModel.SettingsViewModel.Handlers.CollectionChanged += async (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (var handler in args.OldItems.Cast<string>())
                    {
                        await _guiClient.RemoveHandler(handler);
                        Thread.Sleep(1000);
                    }
                }
            };
            this.Dispatcher.Invoke(() =>
            {
                MainWindowViewModel.LogViewModel.Logs.Clear();
                foreach (var logEntry in logs)
                {
                    MainWindowViewModel.LogViewModel.Logs.Add(logEntry);
                }

                var settingsCol = MainWindowViewModel.SettingsViewModel;
                settingsCol.OutputDir = settings.OutputDir;
                settingsCol.LogSource = settings.LogSource;
                settingsCol.LogName = settings.LogName;
                settingsCol.ThumbnailSize = settings.ThumbnailSize;
                MainWindowViewModel.SettingsViewModel.Handlers.Clear();
                foreach (var handler in settings.Handlers)
                {
                    MainWindowViewModel.SettingsViewModel.Handlers.Add(handler);
                }
            });

        }

        public  void SettingsView_Loaded(object sender, RoutedEventArgs e)
        {
            var t = Query();

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            _guiClient?.Dispose();
        }
    }
}
