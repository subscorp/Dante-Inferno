using Communication;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class FirstController : Controller
    {
        private static SettingsModel sm = new SettingsModel();
        private static ImageWebModel iwm = new ImageWebModel();
        private static LogModel lm = new LogModel();

        // GET: First
        // ImageWeb View
        [HttpGet]
        public ActionResult Index()
        {
            iwm.UpdateParameters();
            ViewData.Add("Status", iwm.Status);
            ViewData.Add("Details", iwm.Student_Details);
            ViewData.Add("PhotosNumber", iwm.PhotosNumber);
            return View();
        }

        // Page for asking whether to erase a handler
        public ActionResult Question(string toErase)
        {
            ViewData["ToErase"] = toErase;
            return View();
        }

        // Hides all logs, except mentioned type
        public ActionResult HideTypes(string typeToShow)
        {
            lm.Hide(typeToShow);
            ViewData["Logs"] = lm.Logs;
            return View("Logs");
        }

        // Removes a handler, and updates settings
        public ActionResult RemoveHandler(string handler)
        {
            sm.Remove(handler);

            ViewData["OutputDir"] = sm.Settings.OutputDir;
            ViewData["LogName"] = sm.Settings.LogName;
            ViewData["LogSource"] = sm.Settings.LogSource;
            ViewData["ThumbnailSize"] = sm.Settings.ThumbnailSize;
            ViewData["Handlers"] = sm.Settings.Handlers;

            return View("Config");
        }

        // GET: First/Config
        // Configuration view
        public ActionResult Config()
        {
            sm.GetSettings();
            ViewData["OutputDir"] = sm.Settings.OutputDir;
            ViewData["LogName"] = sm.Settings.LogName;
            ViewData["LogSource"] = sm.Settings.LogSource;
            ViewData["ThumbnailSize"] = sm.Settings.ThumbnailSize;
            ViewData["Handlers"] = sm.Settings.Handlers;
            return View();
        }

        // GET: First/Photos
        // Photos View
        public ActionResult Photos()
        {
            //to avoid photos picking before initializing the settings
            while(sm.Settings.OutputDir == null);

            ViewData["Photos"] = getPictures(); 
            return View();
        }

        //getting the Current source of pictures in the output directory
        private string[] getPictures()
        {
            string[] arr = Directory.GetFiles(Server.MapPath("~/pictures/Thumbnails"), "*", SearchOption.AllDirectories).ToArray();
            int arrLength = arr.Length;

            //calculating number of rows to show, each row has seven items
            double temp = arrLength / 7.0;
            double numRows = Math.Ceiling(temp);

            //initializing the current photo number to zero
            int photoNum = 0;

            string appPath = Server.MapPath("~");

            string[] imgSrc = new string[arr.Length];

            foreach (string s in arr)
            {
                imgSrc[photoNum] = string.Format("{0}", s.Replace(appPath, "").Replace("\\", "/"));
                photoNum++;
            }

            return imgSrc;
        }

        // Removes a photo, and leading back to Photos view
        public ActionResult RemovePhoto(string photo)
        {
            // creating arrays of all photos in photos and thumbnail directories
            string[] arr = Directory.GetFiles(Server.MapPath("~/pictures/photos"), "*", SearchOption.AllDirectories).ToArray();
            string[] arr2 = Directory.GetFiles(Server.MapPath("~/pictures/Thumbnails"), "*", SearchOption.AllDirectories).ToArray();

            //delete the photo
            foreach (var item in arr)
            {
                if (Path.GetFileNameWithoutExtension(item).Equals(photo))
                {
                    string appPath = Server.MapPath("~");
                    string res = string.Format("{0}", item.Replace(appPath, "").Replace("\\", "/"));
                    System.IO.File.Delete(item);
                }
            }

            //delete the corresponding thumbnail
            foreach (var item in arr2)
            {
                if (Path.GetFileNameWithoutExtension(item).Equals(photo))
                {
                    string appPath = Server.MapPath("~");
                    string res = string.Format("{0}", item.Replace(appPath, "").Replace("\\", "/"));
                    System.IO.File.Delete(item);
                }
            }

            ViewData["Photos"] = getPictures();
            return View("Photos");
        }
        
        // Page for asking whether to erase a photo or not
        public ActionResult Delete(string name)
        {
            ViewData["ToErase"] = name;
            return View();
        }

        // Page for viewing photo
        public ActionResult ViewPhoto(string name)
        {
            ViewData["Name"] = name;
            return View();
        }

        // GET: First/Logs
        // logs view
        public ActionResult Logs()
        {
            lm.GetLogs();
            ViewData["Logs"] = lm.Logs;
            return View();
        }

    }
}
