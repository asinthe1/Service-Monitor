namespace ServiceMonitor.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiceStatus", "LastWorkingTime", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ServiceStatus", "LastWorkingTime");
        }
    }
}
