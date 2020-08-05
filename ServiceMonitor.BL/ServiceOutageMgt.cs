using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceMonitor.Entity.Model;

namespace ServiceMonitor.BL
{
    public class ServiceOutageMgt
    {
        public bool IsServiceOutage(int serviceId)
        {
            using (var c = new ServiceDBModel())
            {
                return c.ServiceOutages.Any(x =>
                    x.ServiceId == serviceId && x.StartDateTime <= DateTime.Now && x.EndDateTime >= DateTime.Now);

            }

        }
        public List<ServiceOutage> GetServiceOutagesForService(int serviceId)
        {
            using (var c = new ServiceDBModel())
            {
                return c.ServiceOutages.Where(x => x.ServiceId == serviceId).ToList();
            }

        }

        public List<ServiceOutage> GetAll()
        {
            try
            {
                using (var c = new ServiceDBModel())
                {
                    c.Configuration.LazyLoadingEnabled = false;
                    c.Configuration.ProxyCreationEnabled = false;
                    return c.ServiceOutages.ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ServiceOutage Add(ServiceOutage service)
        {
            try
            {
                using (var c = new ServiceDBModel())
                {
                    c.ServiceOutages.Add(service);
                    c.SaveChanges();
                    return service;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ServiceOutage Update(ServiceOutage service)
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

        public ServiceOutage Delete(ServiceOutage service)
        {
            try
            {
                using (var c = new ServiceDBModel())
                {
                    c.ServiceOutages.Remove(c.ServiceOutages.FirstOrDefault(x => x.Id == service.Id));
                    c.SaveChanges();
                    return service;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ServiceOutage GetById(int id)
        {
            using (var c = new ServiceDBModel())
            {
                c.Configuration.LazyLoadingEnabled = false;
                c.Configuration.ProxyCreationEnabled = false;
                return c.ServiceOutages.FirstOrDefault(x => x.Id == id);
            }
        }

        public List<ServiceOutage> GetByServiceId(int id)
        {
            using (var c = new ServiceDBModel())
            {
                c.Configuration.LazyLoadingEnabled = false;
                c.Configuration.ProxyCreationEnabled = false;
                return c.ServiceOutages.Where(x=>x.ServiceId == id).ToList();
            }
        }
    }
}
