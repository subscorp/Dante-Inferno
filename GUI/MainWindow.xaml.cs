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
using System.Timers;
using Communication;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window//, INotifyPropertyChanged
    {
        //private GUIClient _guiClient;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }

/**        public MainWindowViewModel MainWindowViewModel
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
                        Thread.Sleep(3000);
                    }
                }
            };

   

            await this.Dispatcher.InvokeAsync(() =>
            {
                MainWindowViewModel.LogViewModel.Logs.Clear();

                foreach (var logEntry in logs)
                { 
                    logEntry.Color = MainWindowViewModel.LogViewModel.TypeToColor[logEntry.Type];
                    MainWindowViewModel.LogViewModel.Logs.Add(logEntry);
                }

                MainWindowViewModel.LogViewModel.clearLogs();

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

            System.Timers.Timer t = new System.Timers.Timer(10 * 1000);
            t.Elapsed += async (a, b) =>
            {
                var logs2 = await _guiClient.GetLogs();
                var settings2 = await _guiClient.GetSettings();
                await Dispatcher.InvokeAsync(() =>
                {
                    MainWindowViewModel.LogViewModel.Logs.Clear();
                    foreach (var log in logs2)
                    {
                        log.Color = MainWindowViewModel.LogViewModel.TypeToColor[log.Type];
                        MainWindowViewModel.LogViewModel.Logs.Add(log);
                    }
                    MainWindowViewModel.LogViewModel.clearLogs();

                    MainWindowViewModel.SettingsViewModel.Handlers.Clear();
                    foreach (var handler in settings2.Handlers)
                    {
                        MainWindowViewModel.SettingsViewModel.Handlers.Add(handler);
                    }
                });

            };
            t.AutoReset = true;
            t.Start();

        }

        public  void SettingsView_Loaded(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("ani po o lo?");
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
        }*/
    }
}
