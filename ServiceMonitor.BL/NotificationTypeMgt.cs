using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceMonitor.Entity.Model;

namespace ServiceMonitor.BL
{
    public class NotificationTypeMgt
    {
        public List<NotificationType> GetAll()
        {
            using (var c = new ServiceDBModel())
            {
                c.Configuration.LazyLoadingEnabled = false;
                c.Configuration.ProxyCreationEnabled = false;
                return c.NotificationTypes.ToList();
            }
        }

    }
}
