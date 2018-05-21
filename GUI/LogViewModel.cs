using System.Collections.ObjectModel;

namespace GUI
{
    internal class LogViewModel : ViewModel
    {
        private LogModel lm;

        public string Logs { get; set; }

        public LogViewModel()
        {
            Logs = "bla";
            lm = new LogModel();
        }
    }
}
