using System.Linq;
using ServiceMonitor.Entity.Model;

namespace ServiceMonitor.BL
{
    public class NotificationLogMgt
    {
        public NotificationLog AddNotificationLog(NotificationLog notificationLog)
        {
            using (var c = new ServiceDBModel())
            {
                c.NotificationLogs.Add(notificationLog);
                c.SaveChanges();
                return notificationLog;
            }
        }

        public NotificationLog GetLastNotification(int serviceId)
        {
            using (var c = new ServiceDBModel())
            {
                return c.NotificationLogs.Where(x => x.ServiceId == serviceId).OrderByDescending(o => o.Id)
                    .FirstOrDefault();
            }
        }
    }
}
