namespace GUI
{
    internal class LogModel
    {
        //private mashehou shekashour leTCP

        public string[] settings
        {
            get;
            set;
        }
        public string[] handlers
        {
            get;
            set;
        }
        public LogModel()
        {
            //TODO create connection, get the parameters and use them.
            settings = new string[]{ "12", "path", "output", "aher"};
            handlers = new string[]{ "dever", "herev", "haya"};
        }
    }
}