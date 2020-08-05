namespace ServiceMonitor.Entity.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Notification")]
    public partial class Notification
    {
        public int Id { get; set; }

        public int? ServiceId { get; set; }

        public int? NotificationTypeId { get; set; }

        public int? NotificationMethodId { get; set; }

        [StringLength(250)]
        public string ContactData { get; set; }

        public virtual NotificationMethod NotificationMethod { get; set; }

        public virtual NotificationType NotificationType { get; set; }

        public virtual Service Service { get; set; }
    }
}