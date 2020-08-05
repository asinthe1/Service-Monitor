namespace ServiceMonitor.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Service",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    Description = c.String(maxLength: 250),
                    Host = c.String(nullable: false, maxLength: 50),
                    Port = c.Int(nullable: false),
                    Frequency = c.Int(nullable: false),
                    GraceTime = c.Int(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.ServiceOutage",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ServiceId = c.Int(nullable: false),
                    Reason = c.String(maxLength: 255),
                    StartDateTime = c.DateTime(nullable: false),
                    EndDateTime = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Service", t => t.ServiceId)
                .Index(t => t.ServiceId);

            CreateTable(
                "dbo.ServiceStatus",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ServiceId = c.Int(nullable: false),
                    LastUpdateTime = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Service", t => t.ServiceId)
                .Index(t => t.ServiceId);

            CreateTable(
                "dbo.ServiceStatusLog",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ServiceStatusId = c.Int(nullable: false),
                    StatusId = c.String(nullable: false, maxLength: 10, fixedLength: true),
                    CreatedTime = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServiceStatus", t => t.ServiceStatusId)
                .Index(t => t.ServiceStatusId);

            CreateTable(
                    "dbo.NotificationMethod",
                    c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.NotificationType",
                    c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.Notification",
                    c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServiceId = c.Int(),
                        NotificationTypeId = c.Int(),
                        NotificationMethodId = c.Int(),
                        ContactData = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NotificationMethod", t => t.NotificationMethodId)
                .ForeignKey("dbo.NotificationType", t => t.NotificationTypeId)
                .ForeignKey("dbo.Service", t => t.ServiceId)
                .Index(t => t.ServiceId)
                .Index(t => t.NotificationTypeId)
                .Index(t => t.NotificationMethodId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.ServiceStatus", "ServiceId", "dbo.Service");
            DropForeignKey("dbo.ServiceStatusLog", "ServiceStatusId", "dbo.ServiceStatus");
            DropForeignKey("dbo.ServiceOutage", "ServiceId", "dbo.Service");
            DropForeignKey("dbo.Notification", "ServiceId", "dbo.Service");
            DropForeignKey("dbo.Notification", "NotificationTypeId", "dbo.NotificationType");
            DropForeignKey("dbo.Notification", "NotificationMethodId", "dbo.NotificationMethod");
            DropIndex("dbo.ServiceStatusLog", new[] { "ServiceStatusId" });
            DropIndex("dbo.ServiceStatus", new[] { "ServiceId" });
            DropIndex("dbo.ServiceOutage", new[] { "ServiceId" });
            DropIndex("dbo.Notification", new[] { "NotificationMethodId" });
            DropIndex("dbo.Notification", new[] { "NotificationTypeId" });
            DropIndex("dbo.Notification", new[] { "ServiceId" });
            DropTable("dbo.ServiceStatusLog");
            DropTable("dbo.ServiceStatus");
            DropTable("dbo.ServiceOutage");
            DropTable("dbo.Service");
            DropTable("dbo.NotificationType");
            DropTable("dbo.Notification");
            DropTable("dbo.NotificationMethod");
        }
    }
}