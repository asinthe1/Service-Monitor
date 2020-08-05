namespace ServiceMonitor.Entity.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("NotificationMethod")]
    public partial class NotificationMethod
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }
    }
}