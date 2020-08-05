using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceMonitor.Entity.Model;

namespace ServiceMonitor.BL
{
    public class ServiceStatusMgt
    {
        public List<ServiceStatus> GetByServiceId(int id)
        {
            using (var c = new ServiceDBModel())

            {
                return c.ServiceStatus.Where(x => x.ServiceId == id ).ToList();
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
                    statusChanged = service.IsWorking != isWorking;
                    service.LastUpdateTime = DateTime.UtcNow ;
                    service.IsWorking = isWorking;
                    
                }
                else
                {
                    statusChanged = !isWorking;

                    service = new ServiceStatus
                    {
                        IsWorking = isWorking,
                        LastUpdateTime = DateTime.UtcNow,
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