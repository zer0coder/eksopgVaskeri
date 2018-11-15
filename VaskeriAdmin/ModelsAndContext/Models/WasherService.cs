using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAndContext.Models
{
    public class WasherService
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Service name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Only during daytime?")]
        public bool DaytimeOnly { get; set; }

        [Required]
        [DisplayName("Allowed max reservations?")]
        public int AllowedMaxReservations { get; set; }

        public List<Machine> Machines { get; set; }
        public List<WashTime> WashTimes { get; set; }
        public List<User> Users { get; set; }
    }
}
