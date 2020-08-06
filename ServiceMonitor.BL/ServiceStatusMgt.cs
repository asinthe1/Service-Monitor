using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceMonitor.Entity.Model;

namespace ServiceMonitor.BL
{
    public class ServiceStatusMgt
    {
        public ServiceStatus GetByServiceId(int id)
        {
            using (var c = new ServiceDBModel())

            {
                return c.ServiceStatus.FirstOrDefault(x => x.ServiceId == id );
            }
        }

        public bool UpdateServiceStatus(int serviceId, bool isWorking)
        {
            using (var c = new ServiceDBModel())
            {
                bool statusChanged;
                var service = c.ServiceStatus.FirstOrDefault(x => x.ServiceId == serviceId);
                if (service != null)
                {
                    statusChanged = service.IsWorking != isWorking; // check the service status is changed
                    service.LastUpdateTime = DateTime.UtcNow ;
                    service.LastWorkingTime = isWorking ? DateTime.UtcNow :service.LastWorkingTime;
                    service.IsWorking = isWorking;
                    c.Entry(service).State = EntityState.Modified;
                }
                else
                {
                    statusChanged = !isWorking;

                    service = new ServiceStatus
                    {
                        IsWorking = isWorking,
                        LastUpdateTime = DateTime.UtcNow,
                        LastWorkingTime = isWorking ? DateTime.UtcNow : (DateTime?)null,
                        ServiceId = serviceId
                    };
                    c.ServiceStatus.Add(service);
                }

                c.SaveChanges();

                new ServiceStatusLogMgt().Add(new ServiceStatusLog
                {
                    CreatedTime = DateTime.UtcNow,
                    IsWorking = isWorking,
                    ServiceStatusId = service.Id,
                });
                return statusChanged;
            }

        }
    }
}