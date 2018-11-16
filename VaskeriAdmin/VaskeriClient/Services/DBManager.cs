using ModelsAndContext.Context;
using ModelsAndContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using VaskeriClient.Models;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace VaskeriClient.Services
{
    public class DBManager
    {
        private VaskeriContext db = new VaskeriContext();

        public async Task<bool> Debug_ClearAllMachinesAsync()
        {
            await db.Machines.ForEachAsync(m => m.InUse = false);
            db.SaveChanges();
            return true;
        }

        public bool UserExists(User user)
        {
            User res = db.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);

            if(res != null)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public bool UserCanMakeReservation(User user)
        {
            WasherService service = db.WasherServices.FirstOrDefault(ws => ws.Id == user.ServiceID);

            if(user.NumberOfReservations < service.AllowedMaxReservations)
            {
                return true;
            } else if (user.NumberOfReservations >= service.AllowedMaxReservations)
            {
                return false;
            }

            return false;
        }

        public UserFront GetUserFrontpageInfo(User user)
        {
            UserFront front = new UserFront();

            user = GetUserFull(user);

            if (user != null)
            {
                List<Machine> machines = db.Machines.Where(m => m.ServiceID == user.ServiceID).ToList();
                CheckMachinesInUse(machines);

                front.User = user;
                front.Service = db.WasherServices.FirstOrDefault(s => s.Id == user.ServiceID);
                front.Machines = machines;
                front.Reservations = GetReservations(user);
                front.DoneReservations = GetDoneReservations(user);
            }

            return front;
        }

        public ReservationView GetUnusedMachines(int ServiceID)
        {
            ReservationView viewmodel = new ReservationView();

            viewmodel.Machines = db.Machines.Where(m => m.ServiceID == ServiceID && m.InUse == false).ToList();
            viewmodel.WashTimes = db.WashTimes.Where(wt => wt.ServiceID == ServiceID).ToList();

            return viewmodel;
        }

        public void CreateReservation(string res, string date, string time, User user)
        {
            string[] mid = res.Split(',');
            int[] dato = GetDate(date);

            Reservation reservation = new Reservation();
            reservation.Active = false;
            reservation.UserID = user.Id;
            reservation.Date = new DateTime(dato[0], dato[1], dato[2]);
            reservation.TimeID = Int32.Parse(time);

            List<Machine> list = new List<Machine>();
            for (int i = 0; i < mid.Length; i++)
            {
                int machineid = Int32.Parse(mid[i]);

                Machine machine = db.Machines.FirstOrDefault(m => m.Id == machineid);
                list.Add(machine);
            }
            reservation.Machines = list;

            db.Reservations.Add(reservation);

            User u = db.Users.FirstOrDefault(s => s.Id == user.Id);
            u.NumberOfReservations++;

            db.SaveChanges();
        }

        public StartMachineModel GetAvailableMachines(User user)
        {
            StartMachineModel SMM = new StartMachineModel();

            SMM.DryerPrograms = db.DryerPrograms.Where(dp => dp.ServiceID == user.ServiceID).ToList();
            SMM.WashingPrograms = db.WashingPrograms.Where(dp => dp.ServiceID == user.ServiceID).ToList();


            List<Machine> unusedMachines = db.Machines.Where(m => m.ServiceID == user.ServiceID && m.InUse == false).ToList();
            List<Reservation> reservations = db.Reservations.Include(m => m.Machines).Where(r => r.SID == user.ServiceID && r.Active == false).ToList();

            foreach (var item in reservations)
            {
                unusedMachines.Union(item.Machines);
            }

            List<Machine> dryers = new List<Machine>();
            List<Machine> washers = new List<Machine>();

            foreach (var item in unusedMachines)
            {
                if(item.Type == MachineType.WASHER)
                {
                    washers.Add(item);
                } else
                {
                    dryers.Add(item);
                }
            }

            SMM.WashingMachines = washers;
            SMM.DryerMachines = dryers;

            return SMM;
        }

        public void StartMachine(string mid, string pid, string type, User user)
        {

            User u = db.Users.Include(dr => dr.DoneReservations).FirstOrDefault(l => l.Id == user.Id);

            int machineId = Int32.Parse(mid);
            int programId = Int32.Parse(pid);
            DryerProgram dp = null;
            WashingProgram wp = null;

            Machine mac = db.Machines.FirstOrDefault(m => m.Id == machineId);
            mac.InUse = true;
            mac.StartTime = DateTime.Now;

            if (type == "Dryer")
            {
                dp = db.DryerPrograms.FirstOrDefault(i => i.Id == programId);
                mac.EndTime = DateTime.Now.AddMilliseconds(dp.Length*1000*60);
            }
            else if (type == "Washer")
            {
                wp = db.WashingPrograms.FirstOrDefault(i => i.Id == programId);
                mac.EndTime = DateTime.Now.AddMilliseconds(wp.Length * 1000 * 60);
            }

            Reservation reservation = new Reservation();
            reservation.SID = user.ServiceID;
            reservation.UserID = user.Id;
            reservation.Date = DateTime.Now;
            reservation.TimeID = -1;
            reservation.Finished = true;
            reservation.Machines = new List<Machine>() { mac };

            DoneReservation doneReservation = new DoneReservation();
            doneReservation.UID = user.Id;
            doneReservation.Reservation = reservation;

            if (type == "Dryer")
            {
                doneReservation.DryerProgs = new List<DryerProgram>() { dp };
            }
            else if (type == "Washer")
            {
                doneReservation.WashingProgs = new List<WashingProgram>() { wp };
            }

            u.DoneReservations.Add(doneReservation);

            db.SaveChanges();
        }



        private void CheckMachinesInUse(List<Machine> machines)
        {
            machines.ForEach(m => CheckMachine(m));
        }

        private void CheckMachine(Machine machine)
        {
            if(machine.InUse)
            {
                if(DateTime.Now >= machine.EndTime)
                {
                    machine.InUse = false;
                    db.SaveChanges();
                }
            }
        }

        private int[] GetDate(string date)
        {
            int[] result = new int[3];
            string[] split = date.Split('-');

            result[0] = Int32.Parse(split[0]);
            result[1] = Int32.Parse(split[1]);
            result[2] = Int32.Parse(split[2]);

            return result;
        }

        private User GetUserFull(User user)
        {
            return db.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
        }

        private List<Reservation> GetReservations(User user)
        {
            return db.Reservations.Include(m => m.Machines).Where(u => u.UserID == user.Id).ToList();
        }

        private List<DoneReservation> GetDoneReservations(User user)
        {
            return db.DoneReservations.Include(m => m.DryerProgs).Include(m => m.WashingProgs).Include(r => r.Reservation).Where(u => u.UID == user.Id).ToList();
        }
    }
}