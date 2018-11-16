namespace ModelsAndContext.Migrations
{
    using ModelsAndContext.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ModelsAndContext.Context.VaskeriContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ModelsAndContext.Context.VaskeriContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.


            // Services
            WasherService service1 = new WasherService() { Id = 1, Name = "Vaskeri Tomsen", DaytimeOnly = true, AllowedMaxReservations = 2 };
            WasherService service2 = new WasherService() { Id = 2, Name = "Johns Vaskeri", DaytimeOnly = false, AllowedMaxReservations = 1 };


            // Machines
            Machine machine1 = new Machine() { Id = 1, ServiceID = 1, Number = 1, StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(1), Type = MachineType.WASHER, InUse = true };
            Machine machine2 = new Machine() { Id = 2, ServiceID = 1, Number = 2, Type = MachineType.WASHER, InUse = false };
            Machine machine3 = new Machine() { Id = 3, ServiceID = 1, Number = 3, StartTime = DateTime.Now, EndTime = DateTime.Now.AddMinutes(1), Type = MachineType.DRYER, InUse = true };
            Machine machine4 = new Machine() { Id = 4, ServiceID = 1, Number = 4, Type = MachineType.DRYER, InUse = false };
            Machine machine5 = new Machine() { Id = 5, ServiceID = 1, Number = 5, Type = MachineType.DRYER, InUse = false };

            Machine machine6 = new Machine() { Id = 6, ServiceID = 2, Number = 1, Type = MachineType.WASHER, InUse = true };
            Machine machine7 = new Machine() { Id = 7, ServiceID = 2, Number = 2, Type = MachineType.DRYER, InUse = false };
            Machine machine8 = new Machine() { Id = 8, ServiceID = 2, Number = 3, Type = MachineType.WASHER, InUse = false };


            // Vasketider
            WashTime washTime1 = new WashTime() { Id = 1, ServiceID = 1, Description = "Morgen (6-9)", Length = new TimeSpan(6, 0, 0) };
            WashTime washTime2 = new WashTime() { Id = 2, ServiceID = 1, Description = "Formiddag (9-12)", Length = new TimeSpan(9, 0, 0) };
            WashTime washTime3 = new WashTime() { Id = 3, ServiceID = 1, Description = "Eftermiddag(12-15)", Length = new TimeSpan(12, 0, 0) };
            WashTime washTime4 = new WashTime() { Id = 4, ServiceID = 2, Description = "Morgen (7-10)", Length = new TimeSpan(7, 0, 0) };
            WashTime washTime5 = new WashTime() { Id = 5, ServiceID = 2, Description = "Formiddag (10-12)", Length = new TimeSpan(10, 0, 0) };
            WashTime washTime6 = new WashTime() { Id = 6, ServiceID = 2, Description = "Eftermiddag(12-15)", Length = new TimeSpan(12, 0, 0) };
            WashTime washTime7 = new WashTime() { Id = 7, ServiceID = 2, Description = "Aften (15-20)", Length = new TimeSpan(15, 0, 0) };


            // Machine programs
            WashingProgram washingProgram1 = new WashingProgram() { Id = 1, ServiceID = 1, Name = "Favoritvask 1", Length = 146, ElectricityUsed = 5f, Temperatur = 40, WaterUsed = 25f };
            WashingProgram washingProgram2 = new WashingProgram() { Id = 2, ServiceID = 1, Name = "Uldvask", Length = 180, ElectricityUsed = 4f, Temperatur = 30, WaterUsed = 30f };
            WashingProgram washingProgram3 = new WashingProgram() { Id = 3, ServiceID = 1, Name = "Turbovask", Length = 90, ElectricityUsed = 10f, Temperatur = 60, WaterUsed = 40f };
            WashingProgram washingProgram4 = new WashingProgram() { Id = 4, ServiceID = 2, Name = "Farvevask", Length = 120, ElectricityUsed = 7f, Temperatur = 60, WaterUsed = 40f };
            WashingProgram washingProgram5 = new WashingProgram() { Id = 5, ServiceID = 2, Name = "Turbovask", Length = 90, ElectricityUsed = 5f, Temperatur = 60, WaterUsed = 20f };
            WashingProgram washingProgram6 = new WashingProgram() { Id = 6, ServiceID = 1, Name = "1 Minute", Length = 1, ElectricityUsed = 5f, Temperatur = 60, WaterUsed = 20f };


            DryerProgram dryerProgram1 = new DryerProgram() { Id = 1, ServiceID = 1, Name = "Skabstoert", Length = 150, Temperatur = 55, ElectricityUsed = 5f };
            DryerProgram dryerProgram2 = new DryerProgram() { Id = 2, ServiceID = 1, Name = "Oerken", Length = 125, Temperatur = 75, ElectricityUsed = 8f };
            DryerProgram dryerProgram3 = new DryerProgram() { Id = 3, ServiceID = 2, Name = "Skabstoert+", Length = 150, Temperatur = 70, ElectricityUsed = 5f };
            DryerProgram dryerProgram4 = new DryerProgram() { Id = 4, ServiceID = 2, Name = "QuickDry", Length = 45, Temperatur = 80, ElectricityUsed = 10f };
            DryerProgram dryerProgram5 = new DryerProgram() { Id = 5, ServiceID = 1, Name = "1 Minute", Length = 1, Temperatur = 80, ElectricityUsed = 10f };


            // Users
            User user1 = new User() { Id = 1, ServiceID = 1, Username = "johnd", Password = "abc123", Alias = "John" };
            User user2 = new User() { Id = 2, ServiceID = 1, Username = "toms", Password = "abc123", Alias = "T00mz" };
            User user3 = new User() { Id = 3, ServiceID = 2, Username = "annascan", Password = "abc123", Alias = "Anna" };

            // Reservations FINISHED
            List<Machine> res1m = new List<Machine>
            {
                machine1,
                machine4
            };
            List<Machine> res2m = new List<Machine>
            {
                machine2
            };

            Reservation reservation1 = new Reservation() { Id = 1, UserID = 1, SID = 1, Machines = res1m, Date = DateTime.Now, TimeID = 2, Finished = true };
            Reservation reservation2 = new Reservation() { Id = 2, UserID = 2, SID = 1, Machines = res2m, Date = DateTime.Now, TimeID = 1, Finished = true };

            List<WashingProgram> wps1 = new List<WashingProgram>()
            {
                washingProgram1
            };
            List<WashingProgram> wps2 = new List<WashingProgram>()
            {
                washingProgram5
            };
            List<DryerProgram> dps1 = new List<DryerProgram>()
            {
                dryerProgram2
            };

            DoneReservation done1 = new DoneReservation() { Id = 1, UID = 1, Paid = true, Reservation = reservation1, DryerProgs = dps1, WashingProgs = wps1 };
            DoneReservation done2 = new DoneReservation() { Id = 2, UID = 2, Paid = true, Reservation = reservation2, WashingProgs = wps2 };

            // Add to Database
            context.WasherServices.AddOrUpdate(s => s.Id, service1);
            context.WasherServices.AddOrUpdate(s => s.Id, service2);

            context.Machines.AddOrUpdate(m => m.Id, machine1);
            context.Machines.AddOrUpdate(m => m.Id, machine2);
            context.Machines.AddOrUpdate(m => m.Id, machine3);
            context.Machines.AddOrUpdate(m => m.Id, machine4);
            context.Machines.AddOrUpdate(m => m.Id, machine5);
            context.Machines.AddOrUpdate(m => m.Id, machine6);
            context.Machines.AddOrUpdate(m => m.Id, machine7);
            context.Machines.AddOrUpdate(m => m.Id, machine8);

            context.WashTimes.AddOrUpdate(wt => wt.Id, washTime1);
            context.WashTimes.AddOrUpdate(wt => wt.Id, washTime2);
            context.WashTimes.AddOrUpdate(wt => wt.Id, washTime3);
            context.WashTimes.AddOrUpdate(wt => wt.Id, washTime4);
            context.WashTimes.AddOrUpdate(wt => wt.Id, washTime5);
            context.WashTimes.AddOrUpdate(wt => wt.Id, washTime6);
            context.WashTimes.AddOrUpdate(wt => wt.Id, washTime7);

            context.WashingPrograms.AddOrUpdate(wp => wp.Id, washingProgram1);
            context.WashingPrograms.AddOrUpdate(wp => wp.Id, washingProgram2);
            context.WashingPrograms.AddOrUpdate(wp => wp.Id, washingProgram3);
            context.WashingPrograms.AddOrUpdate(wp => wp.Id, washingProgram4);
            context.WashingPrograms.AddOrUpdate(wp => wp.Id, washingProgram5);
            context.WashingPrograms.AddOrUpdate(wp => wp.Id, washingProgram6);

            context.DryerPrograms.AddOrUpdate(dp => dp.Id, dryerProgram1);
            context.DryerPrograms.AddOrUpdate(dp => dp.Id, dryerProgram2);
            context.DryerPrograms.AddOrUpdate(dp => dp.Id, dryerProgram3);
            context.DryerPrograms.AddOrUpdate(dp => dp.Id, dryerProgram4);
            context.DryerPrograms.AddOrUpdate(dp => dp.Id, dryerProgram5);

            context.Users.AddOrUpdate(s => s.Id, user1);
            context.Users.AddOrUpdate(s => s.Id, user2);
            context.Users.AddOrUpdate(s => s.Id, user3);

            context.Reservations.AddOrUpdate(r => r.Id, reservation1);
            context.Reservations.AddOrUpdate(r => r.Id, reservation2);

            context.DoneReservations.AddOrUpdate(dr => dr.Id, done1);
            context.DoneReservations.AddOrUpdate(dr => dr.Id, done2);
        }
    }
}
