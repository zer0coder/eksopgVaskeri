using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAndContext.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Currently active")]
        public bool Active { get; set; }

        [Required]
        [DisplayName("Machines")]
        public List<Machine> Machines { get; set; }

        [Required]
        [DisplayName("User")]
        public int UserID { get; set; }

        [Required]
        [DisplayName("Service ID")]
        public int SID { get; set; }

        [Required]
        [DisplayName("Date")]
        public DateTime Date { get; set; }

        [Required]
        [DisplayName("Time")]
        public int TimeID { get; set; }

        [DisplayName("Finished?")]
        public bool Finished { get; set; }
    }
}
