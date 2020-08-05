using ServiceMonitor.Entity.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ServiceMonitor.BL
{
    public class ServiceMgt
    {
        public List<Service> GetAllWithDetails()
        {
            try
            {
                using (var c = new ServiceDBModel())
                {
                    c.Configuration.LazyLoadingEnabled = false;
                    c.Configuration.ProxyCreationEnabled = false;
                    var services = c.Services.ToList();

                    foreach (var service in services)
                    {
                        var ServiceStatus = new ServiceStatusMgt().GetByServiceId(service.Id);
                        if (ServiceStatus.FirstOrDefault() != null)
                            service.LastUpdateTime = ServiceStatus.FirstOrDefault().LastUpdateTime;
                    }
                    var serviceList = services.OrderByDescending(o => DateTime.UtcNow - o.LastUpdateTime).ToList();

                    return serviceList;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Service> GetAll()
        {
            try
            {
                using (var c = new ServiceDBModel())
                {
                    c.Configuration.LazyLoadingEnabled = false;
                    c.Configuration.ProxyCreationEnabled = false;
                    return c.Services.ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Service Add(Service service)
        {
            try
            {
                using (var c = new ServiceDBModel())
                {
                    c.Services.Add(service);
                    c.SaveChanges();
                    return service;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Service Update(Service service)
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

        public Service Delete(Service service)
        {
            try
            {
                using (var c = new ServiceDBModel())
                {
                    c.Services.Remove(c.Services.FirstOrDefault(x => x.Id == service.Id));
                    c.SaveChanges();
                    return service;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public Service GetById(int id)
        {
            using (var c = new ServiceDBModel())
            {
                c.Configuration.LazyLoadingEnabled = false;
                c.Configuration.ProxyCreationEnabled = false;
                return c.Services.FirstOrDefault(x => x.Id == id);
            }
        }
    }
}