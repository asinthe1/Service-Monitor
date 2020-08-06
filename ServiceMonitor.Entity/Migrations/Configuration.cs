using ServiceMonitor.Entity.Model;

namespace ServiceMonitor.Entity.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ServiceMonitor.Entity.Model.ServiceDBModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ServiceMonitor.Entity.Model.ServiceDBModel context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            context.NotificationMethods.AddOrUpdate(x => x.Id,
                new NotificationMethod() { Id = 1, Name = "Email" }
            );
            context.NotificationTypes.AddOrUpdate(x => x.Id,
                new NotificationType() { Id = 1, Name = "Service Down" },
                new NotificationType() { Id = 2, Name = "Service Up" },
                new NotificationType() { Id = 3, Name = "Service Up/Down" }
            );
        }
    }
}