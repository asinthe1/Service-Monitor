using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Services.Description;
using ServiceMonitor.BL;
using Service = ServiceMonitor.Entity.Model.Service;

namespace ServiceMonitor.Web.Controllers
{
    public class ServicesController : ApiController
    {
        public HttpResponseMessage GetAllWithDetails()
        {
            var items = new ServiceMgt().GetAllWithDetails();
            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new { items });
        }

        public HttpResponseMessage GetAll()
        {
            var items = new ServiceMgt().GetAll();
            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, items);
        }

        public HttpResponseMessage Delete(int? Id)
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new ServiceMgt().Delete(new Service
            {
                Id = Id.Value
            }));
        }

        public Service Post(Service item)
        {
            return new ServiceMgt().Add(item);
        }

        public Service Put(Service item)
        {
            return new ServiceMgt().Update(item);

        }
    }
}