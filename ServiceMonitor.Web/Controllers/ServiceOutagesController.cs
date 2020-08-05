using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Services.Description;
using ServiceMonitor.BL;
using ServiceOutage = ServiceMonitor.Entity.Model.ServiceOutage;

namespace ServiceMonitor.Web.Controllers
{
    public class ServiceOutagesController : ApiController
    {
        public HttpResponseMessage Get(int id)
        {
            var items = new ServiceOutageMgt().GetByServiceId(id);
            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, items);
        }

        public HttpResponseMessage Delete(int? Id)
        {
            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new ServiceOutageMgt().Delete(new ServiceOutage
            {
                Id = Id.Value
            }));
        }

        public ServiceOutage Post(ServiceOutage item)
        {
            return new ServiceOutageMgt().Add(item);
        }

        public ServiceOutage Put(ServiceOutage item)
        {
            return new ServiceOutageMgt().Update(item);

        }
    }
}