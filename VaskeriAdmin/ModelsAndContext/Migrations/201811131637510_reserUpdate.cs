namespace ModelsAndContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reserUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "SID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "SID");
        }
    }
}
