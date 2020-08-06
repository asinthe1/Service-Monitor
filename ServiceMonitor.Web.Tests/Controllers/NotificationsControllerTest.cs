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
using ServiceMonitor.Entity;
using ServiceMonitor.Entity.Model;
using ServiceMonitor.Web.Controllers;

namespace ServiceMonitor.Web.Tests.Controllers
{
    [TestClass]
    public class NotificationsControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Set up Prerequisites   
            var controller = new NotificationsController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();


            // Act on Test  
            var response = controller.Get(1);
            var contentResult = response as HttpResponseMessage;

            // Assert the result  
            Assert.AreEqual(HttpStatusCode.OK ,contentResult.StatusCode);
            List<Notification> notifications;
            Assert.IsTrue(response.TryGetContentValue<List<Notification>>(out notifications));
        }

        [TestMethod]
        public void Post()
        {
            // Set up Prerequisites   
            var controller = new NotificationsController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act on Test  
            var response = controller.Post(new Notification
            {
                ContactData = "test@domain.com",
                NotificationMethodId = (int)NotificationMethodEnum.Email,
                NotificationTypeId = (int) NotificationTypeEnum.ServiceDown,
                ServiceId = 1
            });

            var contentResult = response as Notification;

            // Assert the result  
            Assert.AreNotEqual(0, contentResult.Id);

        }
    }
}
