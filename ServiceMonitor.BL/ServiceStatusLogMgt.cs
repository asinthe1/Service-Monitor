using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceMonitor.Entity.Model;

namespace ServiceMonitor.BL
{
    public class ServiceStatusLogMgt
    {
        public ServiceStatusLog Add(ServiceStatusLog serviceStatusLog)
        {
            using (var c = new ServiceDBModel())
            {
                c.ServiceStatusLogs.Add(serviceStatusLog);
                c.SaveChanges();
                return serviceStatusLog;
            }
        }

        public ServiceStatusLog GetLastWorkingTimeByServiceId(int id)
        {
            using (var c = new ServiceDBModel())

            {
                return c.ServiceStatusLogs.Where(x => x.ServiceStatus.ServiceId == id && x.IsWorking).OrderByDescending(o=>id).FirstOrDefault();
            }
        }



    }
}
