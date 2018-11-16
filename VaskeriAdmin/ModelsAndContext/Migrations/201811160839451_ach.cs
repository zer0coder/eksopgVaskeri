namespace ModelsAndContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ach : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Machines", "StartTime", c => c.DateTime());
            AlterColumn("dbo.Machines", "EndTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Machines", "EndTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Machines", "StartTime", c => c.DateTime(nullable: false));
        }
    }
}
