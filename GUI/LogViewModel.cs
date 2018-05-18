using System.Collections.ObjectModel;

namespace GUI
{
    internal class LogViewModel : ViewModel
    {
        private LogModel lm;
        
        public LogViewModel()
        {
            lm = new LogModel();
        }
    }
}
