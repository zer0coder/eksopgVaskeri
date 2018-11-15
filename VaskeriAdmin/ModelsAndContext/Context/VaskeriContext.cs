using ModelsAndContext.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAndContext.Context
{
    public class VaskeriContext : DbContext
    {
        public VaskeriContext() : base("name=WasherServiceDb") { }

        public DbSet<WasherService> WasherServices { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<WashTime> WashTimes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WashingProgram> WashingPrograms { get; set; }
        public DbSet<DryerProgram> DryerPrograms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<DoneReservation> DoneReservations { get; set; }
    }
}
