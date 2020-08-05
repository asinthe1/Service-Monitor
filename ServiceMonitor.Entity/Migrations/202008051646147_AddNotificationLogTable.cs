namespace ServiceMonitor.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotificationLogTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NotificationLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServiceId = c.Int(),
                        NotificationMethodId = c.Int(),
                        ContactData = c.String(maxLength: 250),
                        SentDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NotificationMethod", t => t.NotificationMethodId)
                .ForeignKey("dbo.Service", t => t.ServiceId)
                .Index(t => t.ServiceId)
                .Index(t => t.NotificationMethodId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NotificationLog", "ServiceId", "dbo.Service");
            DropForeignKey("dbo.NotificationLog", "NotificationMethodId", "dbo.NotificationMethod");
            DropIndex("dbo.NotificationLog", new[] { "NotificationMethodId" });
            DropIndex("dbo.NotificationLog", new[] { "ServiceId" });
            DropTable("dbo.NotificationLog");
        }
    }
}
