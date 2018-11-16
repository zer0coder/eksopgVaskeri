namespace ModelsAndContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DoneReservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UID = c.Int(nullable: false),
                        Paid = c.Boolean(nullable: false),
                        Reservation_Id = c.Int(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Reservations", t => t.Reservation_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Reservation_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.DryerPrograms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServiceID = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Length = c.Int(nullable: false),
                        Temperatur = c.Int(nullable: false),
                        ElectricityUsed = c.Single(nullable: false),
                        DoneReservation_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DoneReservations", t => t.DoneReservation_Id)
                .Index(t => t.DoneReservation_Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Active = c.Boolean(nullable: false),
                        UserID = c.Int(nullable: false),
                        SID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        TimeID = c.Int(nullable: false),
                        Finished = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Machines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        InUse = c.Boolean(nullable: false),
                        ServiceID = c.Int(nullable: false),
                        DryerProg_Id = c.Int(),
                        WashingProg_Id = c.Int(),
                        WasherService_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DryerPrograms", t => t.DryerProg_Id)
                .ForeignKey("dbo.WashingPrograms", t => t.WashingProg_Id)
                .ForeignKey("dbo.WasherServices", t => t.WasherService_Id)
                .Index(t => t.DryerProg_Id)
                .Index(t => t.WashingProg_Id)
                .Index(t => t.WasherService_Id);
            
            CreateTable(
                "dbo.WashingPrograms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WaterUsed = c.Single(nullable: false),
                        ServiceID = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Length = c.Int(nullable: false),
                        Temperatur = c.Int(nullable: false),
                        ElectricityUsed = c.Single(nullable: false),
                        DoneReservation_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DoneReservations", t => t.DoneReservation_Id)
                .Index(t => t.DoneReservation_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServiceID = c.Int(nullable: false),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Alias = c.String(),
                        NumberOfReservations = c.Int(nullable: false),
                        Konti = c.Single(nullable: false),
                        WasherService_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WasherServices", t => t.WasherService_Id)
                .Index(t => t.WasherService_Id);
            
            CreateTable(
                "dbo.WasherServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        DaytimeOnly = c.Boolean(nullable: false),
                        AllowedMaxReservations = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WashTimes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        Length = c.Time(nullable: false, precision: 7),
                        ServiceID = c.Int(nullable: false),
                        WasherService_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WasherServices", t => t.WasherService_Id)
                .Index(t => t.WasherService_Id);
            
            CreateTable(
                "dbo.MachineReservations",
                c => new
                    {
                        Machine_Id = c.Int(nullable: false),
                        Reservation_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Machine_Id, t.Reservation_Id })
                .ForeignKey("dbo.Machines", t => t.Machine_Id, cascadeDelete: true)
                .ForeignKey("dbo.Reservations", t => t.Reservation_Id, cascadeDelete: true)
                .Index(t => t.Machine_Id)
                .Index(t => t.Reservation_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WashTimes", "WasherService_Id", "dbo.WasherServices");
            DropForeignKey("dbo.Users", "WasherService_Id", "dbo.WasherServices");
            DropForeignKey("dbo.Machines", "WasherService_Id", "dbo.WasherServices");
            DropForeignKey("dbo.Reservations", "UserID", "dbo.Users");
            DropForeignKey("dbo.DoneReservations", "User_Id", "dbo.Users");
            DropForeignKey("dbo.WashingPrograms", "DoneReservation_Id", "dbo.DoneReservations");
            DropForeignKey("dbo.DoneReservations", "Reservation_Id", "dbo.Reservations");
            DropForeignKey("dbo.Machines", "WashingProg_Id", "dbo.WashingPrograms");
            DropForeignKey("dbo.MachineReservations", "Reservation_Id", "dbo.Reservations");
            DropForeignKey("dbo.MachineReservations", "Machine_Id", "dbo.Machines");
            DropForeignKey("dbo.Machines", "DryerProg_Id", "dbo.DryerPrograms");
            DropForeignKey("dbo.DryerPrograms", "DoneReservation_Id", "dbo.DoneReservations");
            DropIndex("dbo.MachineReservations", new[] { "Reservation_Id" });
            DropIndex("dbo.MachineReservations", new[] { "Machine_Id" });
            DropIndex("dbo.WashTimes", new[] { "WasherService_Id" });
            DropIndex("dbo.Users", new[] { "WasherService_Id" });
            DropIndex("dbo.WashingPrograms", new[] { "DoneReservation_Id" });
            DropIndex("dbo.Machines", new[] { "WasherService_Id" });
            DropIndex("dbo.Machines", new[] { "WashingProg_Id" });
            DropIndex("dbo.Machines", new[] { "DryerProg_Id" });
            DropIndex("dbo.Reservations", new[] { "UserID" });
            DropIndex("dbo.DryerPrograms", new[] { "DoneReservation_Id" });
            DropIndex("dbo.DoneReservations", new[] { "User_Id" });
            DropIndex("dbo.DoneReservations", new[] { "Reservation_Id" });
            DropTable("dbo.MachineReservations");
            DropTable("dbo.WashTimes");
            DropTable("dbo.WasherServices");
            DropTable("dbo.Users");
            DropTable("dbo.WashingPrograms");
            DropTable("dbo.Machines");
            DropTable("dbo.Reservations");
            DropTable("dbo.DryerPrograms");
            DropTable("dbo.DoneReservations");
        }
    }
}
