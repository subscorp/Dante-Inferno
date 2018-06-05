using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public abstract class WebModel
    {
        protected GUIClient client;

        public WebModel()
        {
            client = GUIClient.Instance;
            client.Connect();
        }
    }
}