using ModelsAndContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VaskeriClient.Models
{
    public class ReservationView
    {
        public List<Machine> Machines { get; set; }
        public List<WashTime> WashTimes { get; set; }
    }
}