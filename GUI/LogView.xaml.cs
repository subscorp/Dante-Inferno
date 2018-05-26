using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Communication;

namespace GUI
{
    /// <summary>
    /// Interaction logic for LogView.xaml
    /// </summary>
    public partial class LogView : UserControl
    {
        
        public LogView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty LogViewModelProperty = DependencyProperty.Register(
            "LogViewModel", typeof(LogViewModel), typeof(LogView), new PropertyMetadata(default(LogViewModel)));

        public LogViewModel LogViewModel
        {
            get { return (LogViewModel) GetValue(LogViewModelProperty); }
            set { SetValue(LogViewModelProperty, value); }
        }


        private void LogView_OnLoaded(object sender, RoutedEventArgs e)
        {
            var ss = 4;
        }
    }
}
