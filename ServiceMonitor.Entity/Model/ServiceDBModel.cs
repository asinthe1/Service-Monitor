using ServiceMonitor.Entity.Migrations;

namespace ServiceMonitor.Entity.Model
{
    using System.Data.Entity;

    public partial class ServiceDBModel : DbContext
    {
        public ServiceDBModel()
            : base("name=ServiceDBModel")
        {
        }

        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<NotificationMethod> NotificationMethods { get; set; }
        public virtual DbSet<NotificationType> NotificationTypes { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServiceOutage> ServiceOutages { get; set; }
        public virtual DbSet<ServiceStatus> ServiceStatus { get; set; }
        public virtual DbSet<ServiceStatusLog> ServiceStatusLogs { get; set; }
        public virtual DbSet<NotificationLog> NotificationLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Service>()
                .HasMany(e => e.ServiceOutages)
                .WithRequired(e => e.Service)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.ServiceStatus)
                .WithRequired(e => e.Service)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ServiceStatus>()
                .HasMany(e => e.ServiceStatusLogs)
                .WithRequired(e => e.ServiceStatus)
                .HasForeignKey(e => e.ServiceStatusId)
                .WillCascadeOnDelete(false);

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ServiceDBModel, Configuration>()); //initialize database for new instance

        }
    }
}