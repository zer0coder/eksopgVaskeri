namespace ModelsAndContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Machines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Reserved = c.Boolean(nullable: false),
                        Service_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WasherServices", t => t.Service_Id)
                .Index(t => t.Service_Id);
            
            CreateTable(
                "dbo.WasherServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        DaytimeOnly = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Alias = c.String(),
                        Service_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WasherServices", t => t.Service_Id)
                .Index(t => t.Service_Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Active = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Machine_Id = c.Int(nullable: false),
                        Time_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Machines", t => t.Machine_Id, cascadeDelete: true)
                .ForeignKey("dbo.WashTimes", t => t.Time_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Machine_Id)
                .Index(t => t.Time_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.WashTimes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        Start = c.Time(nullable: false, precision: 7),
                        End = c.Time(nullable: false, precision: 7),
                        Service_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WasherServices", t => t.Service_Id)
                .Index(t => t.Service_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Service_Id", "dbo.WasherServices");
            DropForeignKey("dbo.Reservations", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Reservations", "Time_Id", "dbo.WashTimes");
            DropForeignKey("dbo.WashTimes", "Service_Id", "dbo.WasherServices");
            DropForeignKey("dbo.Reservations", "Machine_Id", "dbo.Machines");
            DropForeignKey("dbo.Machines", "Service_Id", "dbo.WasherServices");
            DropIndex("dbo.WashTimes", new[] { "Service_Id" });
            DropIndex("dbo.Reservations", new[] { "User_Id" });
            DropIndex("dbo.Reservations", new[] { "Time_Id" });
            DropIndex("dbo.Reservations", new[] { "Machine_Id" });
            DropIndex("dbo.Users", new[] { "Service_Id" });
            DropIndex("dbo.Machines", new[] { "Service_Id" });
            DropTable("dbo.WashTimes");
            DropTable("dbo.Reservations");
            DropTable("dbo.Users");
            DropTable("dbo.WasherServices");
            DropTable("dbo.Machines");
        }
    }
}
