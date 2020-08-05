using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceMonitor.BL;
using ServiceMonitor.Entity.Model;

namespace ServiceMonitor.Web.Controllers
{
    public class ServiceOutageController : Controller
    {
        // GET: Service
        public ActionResult _ServiceOutage(int id, string callback)
        {
            return PartialView(id);
        }

        public ActionResult _Add(int id, string callback)
        {
            ViewBag.Callback = callback;
            var service = new ServiceOutage{ServiceId = id};
            return PartialView(service);
        }

        public ActionResult _Edit(int id, string callback)
        {
            ViewBag.Callback = callback;
            return PartialView(new ServiceOutageMgt().GetById(id));
        }

        public ActionResult _Delete(int id, string callback)
        {
            ViewBag.Callback = callback;
            return PartialView(new ServiceOutageMgt().GetById(id));
        }
    }
}