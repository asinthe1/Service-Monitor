namespace ServiceMonitor.Entity.Model
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ServiceStatusLog")]
    public partial class ServiceStatusLog
    {
        public int Id { get; set; }

        public int ServiceStatusId { get; set; }

        public bool IsWorking { get; set; }

        public DateTime CreatedTime { get; set; }

        public virtual ServiceStatus ServiceStatus { get; set; }
    }
}