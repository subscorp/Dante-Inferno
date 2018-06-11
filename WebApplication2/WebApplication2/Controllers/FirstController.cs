﻿using Communication;
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
        [HttpGet]
        public ActionResult Index()
        {
            iwm.UpdateParameters();
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

        public ActionResult HideTypes(string typeToShow)
        {
            lm.Hide(typeToShow);
            ViewData["Logs"] = lm.Logs;
            return View("Logs");
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
            sm.GetSettings();
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
            //to avoid photos picking before initializing the settings
            while(sm.Settings.OutputDir == null);

            ViewData["OutputDir"] = sm.Settings.OutputDir; 
            return View();
        }

        public ActionResult RemovePhoto(string photo)
        {
            string[] arr = Directory.GetFiles(Server.MapPath("~/pictures/photos"), "*", SearchOption.AllDirectories).ToArray();
            string[] arr2 = Directory.GetFiles(Server.MapPath("~/pictures/Thumbnails"), "*", SearchOption.AllDirectories).ToArray();

            foreach (var item in arr)
            {
                if (Path.GetFileNameWithoutExtension(item).Equals(photo))
                {
                    string appPath = Server.MapPath("~");
                    string res = string.Format("{0}", item.Replace(appPath, "").Replace("\\", "/"));
                    System.IO.File.Delete(item);
                }
            }

            foreach (var item in arr2)
            {
                if (Path.GetFileNameWithoutExtension(item).Equals(photo))
                {
                    string appPath = Server.MapPath("~");
                    string res = string.Format("{0}", item.Replace(appPath, "").Replace("\\", "/"));
                    System.IO.File.Delete(item);
                }
            }
            
            return View("Photos");
        }

        public ActionResult Delete(string name)
        {
            ViewData["ToErase"] = name;
            return View();
        }

        public ActionResult ViewPhoto(string name)
        {
            ViewData["Name"] = name;
            return View();
        }

        // GET: First/Logs
        public ActionResult Logs()
        {
            lm.GetLogs();
            ViewData["Logs"] = lm.Logs;
            return View();
        }

    }
}
