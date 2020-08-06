using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceMonitor.Entity.Model;
using ServiceMonitor.Web.Controllers;

namespace ServiceMonitor.Web.Tests.Controllers
{
    [TestClass]
    public class NotificationMethodsControllerTest
    {
        [TestMethod]
        public void GetAll()
        {
            // Set up Prerequisites   
            var controller = new NotificationMethodsController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();


            // Act on Test  
            var response = controller.GetAll();
            var contentResult = response as HttpResponseMessage;

            // Assert the result  
            Assert.AreEqual(HttpStatusCode.OK ,contentResult.StatusCode);
            List<NotificationMethod> notificationMethods;
            Assert.IsTrue(response.TryGetContentValue<List<NotificationMethod>>(out notificationMethods));
            Assert.AreEqual(1, notificationMethods.Count);
        }
    }
}
