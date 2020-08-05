using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceMonitor.Entity.Model;

namespace ServiceMonitor.BL
{
    public class NotificationMgt
    {
        public List<Notification> GetNotificationsForService(int serviceId)
        {
            using (var c = new ServiceDBModel())
            {
                return c.Notifications.Where(x => x.ServiceId == serviceId).ToList();
            }

        }

        public List<Notification> GetAll()
        {
            try
            {
                using (var c = new ServiceDBModel())
                {
                    c.Configuration.LazyLoadingEnabled = false;
                    c.Configuration.ProxyCreationEnabled = false;
                    return c.Notifications.ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Notification Add(Notification service)
        {
            try
            {
                using (var c = new ServiceDBModel())
                {
                    c.Notifications.Add(service);
                    c.SaveChanges();
                    return service;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Notification Update(Notification service)
        {
            try
            {
                using (var c = new ServiceDBModel())
                {
                    c.Entry(service).State = EntityState.Modified;
                    c.SaveChanges();
                    return service;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Notification Delete(Notification service)
        {
            try
            {
                using (var c = new ServiceDBModel())
                {
                    c.Notifications.Remove(c.Notifications.FirstOrDefault(x => x.Id == service.Id));
                    c.SaveChanges();
                    return service;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Notification GetById(int id)
        {
            using (var c = new ServiceDBModel())
            {
                c.Configuration.LazyLoadingEnabled = false;
                c.Configuration.ProxyCreationEnabled = false;
                return c.Notifications.FirstOrDefault(x => x.Id == id);
            }
        }

        public List<Notification> GetByServiceId(int id)
        {
            using (var c = new ServiceDBModel())
            {
                c.Configuration.LazyLoadingEnabled = false;
                c.Configuration.ProxyCreationEnabled = false;
                return c.Notifications.Where(x=>x.ServiceId == id).ToList();
            }
        }
    }
}
