using System;

namespace ServiceMonitor.Entity.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Service")]
    public partial class Service
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string Host { get; set; }

        public int Port { get; set; }

        public int Frequency { get; set; }

        public int? GraceTime { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }

        public virtual ICollection<ServiceOutage> ServiceOutages { get; set; }

        public virtual ICollection<ServiceStatus> ServiceStatus { get; set; }

        [NotMapped]
        public DateTime LastUpdateTime { get; set; }

        [NotMapped]
        public DateTime? LastWorkingTime { get; set; }

        [NotMapped]
        public int ErrorLevel => 10;

        [NotMapped]
        public int WarningLevel => 5;
    }
}