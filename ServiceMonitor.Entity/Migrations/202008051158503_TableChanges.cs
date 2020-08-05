namespace ServiceMonitor.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiceStatus", "IsWorking", c => c.Boolean(nullable: false));
            AddColumn("dbo.ServiceStatusLog", "IsWorking", c => c.Boolean(nullable: false));
            DropColumn("dbo.ServiceStatusLog", "StatusId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ServiceStatusLog", "StatusId", c => c.String(nullable: false, maxLength: 10, fixedLength: true));
            DropColumn("dbo.ServiceStatusLog", "IsWorking");
            DropColumn("dbo.ServiceStatus", "IsWorking");
        }
    }
}
