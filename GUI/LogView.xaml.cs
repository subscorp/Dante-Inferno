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

        /// <summary>
        /// Initializes a new instance of the <see cref="LogView"/> class.
        /// </summary>
        public LogView()
        {
            InitializeComponent();
            this.DataContext = new LogViewModel();
        }

    }
}
