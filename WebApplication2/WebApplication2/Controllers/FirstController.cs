using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class FirstController : Controller
    {

        // GET: First
        [HttpGet]
        public ActionResult Index()
        {
            ImageWebModel iwm = new ImageWebModel();
            ViewData.Add("Status", iwm.Status);
            ViewData.Add("Details", iwm.Student_Details);
            ViewData.Add("PhotosNumber", iwm.PhotosNumber);
            return View();
        }

        public ActionResult Question()
        {
            return View();
        }

        // GET: First/Config
        public ActionResult Config()
        {
            SettingsModel sm = new SettingsModel();

            ViewData["OutputDir"] = sm.Settings.OutputDir;
            ViewData["LogName"] = sm.Settings.LogName;
            ViewData["LogSource"] = sm.Settings.LogSource;
            ViewData["ThumbnailSize"] = sm.Settings.ThumbnailSize;
            ViewData["Handlers"] = sm.Settings.Handlers;
            return View();
        }

        // GET: First/Photos
        public ActionResult Photos()
        {
            return View();
        }

        // GET: First/Logs
        public ActionResult Logs()
        {
            LogModel lg = new LogModel();
            ViewData["Logs"] = lg.Logs[0].Message;
            return View();
        }

    }
}
