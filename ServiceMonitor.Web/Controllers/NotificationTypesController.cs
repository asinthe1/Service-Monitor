using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Services.Description;
using ServiceMonitor.BL;
using Notification = ServiceMonitor.Entity.Model.Notification;

namespace ServiceMonitor.Web.Controllers
{
    public class NotificationTypesController : ApiController
    {
        public HttpResponseMessage GetAll()
        {
            var items = new NotificationTypeMgt().GetAll();
            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, items);
        }

    }
}