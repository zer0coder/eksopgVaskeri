namespace ModelsAndContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class machjin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Machines", "StartTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Machines", "EndTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Machines", "EndTime");
            DropColumn("dbo.Machines", "StartTime");
        }
    }
}
