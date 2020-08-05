using System;

namespace ServiceMonitor.Entity.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("NotificationLog")]
    public partial class NotificationLog
    {
        public int Id { get; set; }

        public int? ServiceId { get; set; }

        public int NotificationId { get; set; }

        public DateTime SentDateTime { get; set; }

        public bool IsWorking { get; set; }
        public virtual Notification Notification { get; set; }


        public virtual Service Service { get; set; }
    }
}