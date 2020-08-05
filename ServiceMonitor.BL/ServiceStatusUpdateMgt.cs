using ServiceMonitor.Entity;
using ServiceMonitor.Entity.Model;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;

namespace ServiceMonitor.BL
{
    public class ServiceStatusUpdateMgt
    {
        public void Process(object o)
        {
            var service = (Service)o;
            using (TcpClient tcpClient = new TcpClient())
            {
                try
                {
                    tcpClient.Connect(service.Host, service.Port);
                    if (new ServiceStatusMgt().UpdateServiceStatus(service.Id, true))
                        SendNotification(service, true);
                }
                catch (Exception e)
                {
                    if (new ServiceStatusMgt().UpdateServiceStatus(service.Id, false))
                        SendNotification(service, false);

                    if (new ServiceStatusLogMgt().GetLastWorkingTimeByServiceId(service.Id)?.CreatedTime
                        .AddSeconds(service.GraceTime.GetValueOrDefault(0)) <= DateTime.UtcNow)
                    {
                        var lastNotification = new NotificationLogMgt().GetLastNotification(service.Id);
                        if (lastNotification != null && !lastNotification.IsWorking)
                        {
                            SendNotification(service, false);
                        }
                    }


                }
            }
        }

        public void SendNotification(Service service, bool isWorking)
        {
            if (!new ServiceOutageMgt().IsServiceOutage(service.Id))
            {
                var notificions = new NotificationMgt().GetNotificationsForService(service.Id);
                foreach (var notification in notificions)
                {
                    if (notification.NotificationMethodId == (int)NotificationMethodEnum.Email)
                    {
                        if ((notification.NotificationTypeId == (int)NotificationTypeEnum.ServiceUp ||
                             notification.NotificationTypeId == (int)NotificationTypeEnum.ServiceUpDown)
                            && isWorking)
                        {
                            SendEmail(notification.ContactData, service.Name, isWorking);
                            new NotificationLogMgt().AddNotificationLog(new NotificationLog
                            {
                                IsWorking = isWorking,
                                NotificationId = notification.Id,
                                SentDateTime = DateTime.UtcNow,
                                ServiceId = notification.ServiceId

                            });
                        }

                        if ((notification.NotificationTypeId == (int) NotificationTypeEnum.ServiceDown ||
                             notification.NotificationTypeId == (int) NotificationTypeEnum.ServiceUpDown)
                            && !isWorking)
                        {

                            SendEmail(notification.ContactData, service.Name, isWorking);
                            new NotificationLogMgt().AddNotificationLog(new NotificationLog
                            {
                                IsWorking = isWorking,
                                NotificationId = notification.Id,
                                SentDateTime = DateTime.UtcNow,
                                ServiceId = notification.ServiceId

                            });
                        }
                    }
                }
            }

        }


        public void SendEmail(string emailAddress, string serviceName, bool isWorking)
        {
            NameValueCollection appSettings = ConfigurationManager.AppSettings;

            var fromAddress = new MailAddress(appSettings["EmailAddress"], "Service Notification");
            var toAddress = new MailAddress(emailAddress, emailAddress);
            string fromPassword = appSettings["Password"].ToString();
            string subject = serviceName + (isWorking ? " Working Now" : " Not Working");
            string body = isWorking ? "Service Back to work at " : "Service Goes Down at " + DateTime.Now.ToString(@"yyyy/MM/dd hh:mm:ss tt", new CultureInfo("en-US"));

            var smtp = new SmtpClient
            {
                Host = appSettings["SmtpHost"],
                Port = Convert.ToInt32(appSettings["SmtpPort"]),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }


    }
}
