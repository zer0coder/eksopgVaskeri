using ModelsAndContext.Context;
using ModelsAndContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace VaskeriAdmin
{
    public class DatabaseManager
    {
        private VaskeriContext db = new VaskeriContext();

        public void AddNewUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }

        public List<WasherService> GetWasherServices()
        {
            return db.WasherServices.ToList();
        }

        public List<User> GetUsers(WasherService service)
        {
            return db.Users.Where(s => s.ServiceID == service.Id).ToList();
        }

        public List<DoneReservation> GetDoneReservations(User user)
        {
            //return db.Reservations.Include(m => m.Machine).Include(dp => dp.DryerProg).Include(wp => wp.WashingProg).Where(u => u.UserID == user.Id).ToList();
            return db.DoneReservations.Include(m => m.DryerProgs).Include(m => m.WashingProgs).Include(r => r.Reservation).Where(u => u.UID == user.Id).ToList();
        }
    }
}
