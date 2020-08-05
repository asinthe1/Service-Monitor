namespace ServiceMonitor.Entity.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ServiceOutage")]
    public partial class ServiceOutage
    {
        public int Id { get; set; }

        public int ServiceId { get; set; }

        [StringLength(255)]
        public string Reason { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public virtual Service Service { get; set; }
    }
}