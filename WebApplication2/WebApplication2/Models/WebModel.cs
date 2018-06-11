using Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    /// <summary>
    /// Class for model of a web page.
    /// </summary>
    public abstract class WebModel
    {
        protected GUIClient client;

        //Constructor - establishing client connection
        public WebModel()
        {
            client = GUIClient.Instance;
            client.Connect();
        }
    }
}