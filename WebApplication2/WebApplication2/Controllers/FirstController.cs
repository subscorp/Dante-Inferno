using Communication;
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
        private static SettingsModel sm = new SettingsModel();
        private static ImageWebModel iwm = new ImageWebModel();
        private static LogModel lm = new LogModel();

        // GET: First
        [HttpGet]
        public ActionResult Index()
        {
            ViewData.Add("Status", iwm.Status);
            ViewData.Add("Details", iwm.Student_Details);
            ViewData.Add("PhotosNumber", iwm.PhotosNumber);
            return View();
        }

        public ActionResult Question(string toErase)
        {
            ViewData["ToErase"] = toErase;
            return View();
        }

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
        public ActionResult Config()
        {
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
            ViewData["Logs"] = lm.Logs[0].Message;
            return View();
        }

    }
}
