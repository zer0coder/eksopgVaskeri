using ModelsAndContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VaskeriClient.Models
{
    public class UserFront
    {
        public User User { get; set; }
        public WasherService Service { get; set; }
        public List<Machine> Machines { get; set; }
        public List<Reservation> Reservations { get; set; }
        public List<DoneReservation> DoneReservations { get; set; }

        public float TotalCost
        {
            get
            {
                float total = 0f;
                if (DoneReservations != null)
                {
                    foreach (var item in DoneReservations)
                    {
                        total += item.Cost;
                    }
                }

                return total;
            }
        }
    }
}