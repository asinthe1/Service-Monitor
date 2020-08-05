using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceMonitor.BL;
using ServiceMonitor.Entity.Model;

namespace ServiceMonitor.Web.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _Add(string callback)
        {
            ViewBag.Callback = callback;
            var service = new Service();
            return PartialView(service);
        }

        public ActionResult _Edit(int id, string callback)
        {
            ViewBag.Callback = callback;
            return PartialView(new ServiceMgt().GetById(id));
        }

        public ActionResult _Delete(int id, string callback)
        {
            ViewBag.Callback = callback;
            return PartialView(new ServiceMgt().GetById(id));
        }
    }
}