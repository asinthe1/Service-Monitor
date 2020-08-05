namespace ServiceMonitor.Entity.Model
{
    using System;
    using System.Collections.Generic;

    public partial class ServiceStatus
    {
        public int Id { get; set; }

        public int ServiceId { get; set; }
        public bool IsWorking { get; set; }

        public DateTime LastUpdateTime { get; set; }

        public virtual Service Service { get; set; }

        public virtual ICollection<ServiceStatusLog> ServiceStatusLogs { get; set; }
    }
}