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

        public void DeleteUser(User user)
        {
            List<DoneReservation> done = db.DoneReservations.Where(dr => dr.UID == user.Id).ToList();
            List<Reservation> res = db.Reservations.Where(r => r.UserID == user.Id).ToList();

            if (done.Count > 0)
            {
                done.ForEach(d => db.DoneReservations.Remove(d));
            }
            if (res.Count > 0)
            {
                res.ForEach(r => db.Reservations.Remove(r));
            }

            User removeMe = db.Users.FirstOrDefault(u => u.Id == user.Id);

            db.Users.Remove(removeMe);
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
            return db.DoneReservations.Include(m => m.DryerProgs).Include(m => m.WashingProgs).Include(r => r.Reservation).Where(u => u.UID == user.Id).ToList();
        }

        public void ClearKonti(User user)
        {
            User us = db.Users.Include(dr => dr.DoneReservations).FirstOrDefault(item => item.Id == user.Id);

            if(us != null)
            {
                // I made an error while making the class models for the db.
                foreach (var item in us.DoneReservations)
                {
                    DoneReservation dr = db.DoneReservations.FirstOrDefault(i => i.Id == item.Id);
                    dr.Paid = true;
                    db.SaveChanges();
                }
            }
        }
    }
}
