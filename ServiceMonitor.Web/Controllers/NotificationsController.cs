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
    public class NotificationsController : ApiController
    {
        public HttpResponseMessage Get(int id)
        {
            var items = new NotificationMgt().GetByServiceId(id);
            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, items);
        }

        public HttpResponseMessage Delete(int? Id)
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new NotificationMgt().Delete(new Notification
            {
                Id = Id.Value
            }));
        }

        public Notification Post(Notification item)
        {
            return new NotificationMgt().Add(item);
        }

        public Notification Put(Notification item)
        {
            return new NotificationMgt().Update(item);

        }
    }
}