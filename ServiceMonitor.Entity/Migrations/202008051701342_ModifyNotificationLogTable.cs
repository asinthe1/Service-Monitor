namespace ServiceMonitor.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyNotificationLogTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NotificationLog", "NotificationMethodId", "dbo.NotificationMethod");
            DropIndex("dbo.NotificationLog", new[] { "NotificationMethodId" });
            AddColumn("dbo.NotificationLog", "NotificationId", c => c.Int(nullable: false));
            AddColumn("dbo.NotificationLog", "IsWorking", c => c.Boolean(nullable: false));
            CreateIndex("dbo.NotificationLog", "NotificationId");
            AddForeignKey("dbo.NotificationLog", "NotificationId", "dbo.Notification", "Id", cascadeDelete: true);
            DropColumn("dbo.NotificationLog", "NotificationMethodId");
            DropColumn("dbo.NotificationLog", "ContactData");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NotificationLog", "ContactData", c => c.String(maxLength: 250));
            AddColumn("dbo.NotificationLog", "NotificationMethodId", c => c.Int());
            DropForeignKey("dbo.NotificationLog", "NotificationId", "dbo.Notification");
            DropIndex("dbo.NotificationLog", new[] { "NotificationId" });
            DropColumn("dbo.NotificationLog", "IsWorking");
            DropColumn("dbo.NotificationLog", "NotificationId");
            CreateIndex("dbo.NotificationLog", "NotificationMethodId");
            AddForeignKey("dbo.NotificationLog", "NotificationMethodId", "dbo.NotificationMethod", "Id");
        }
    }
}
