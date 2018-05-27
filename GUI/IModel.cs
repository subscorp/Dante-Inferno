using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace GUI
{
    internal abstract class IModel : INotifyPropertyChanged
    {
        protected GUIClient _guiClient;

        public IModel()
        {
            var t = Query();
            Console.WriteLine("am connected birch!");

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public async Task Query()
        {
            _guiClient = GUIClient.Instance;
            await _guiClient.Connect();
            Console.WriteLine("me connect");
        }


        }
    }