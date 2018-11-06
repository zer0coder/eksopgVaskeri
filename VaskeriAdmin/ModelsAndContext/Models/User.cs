using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ModelsAndContext.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Username")]
        public string Username { get; set; }

        [Required]
        [DisplayName("Password")]
        public string Password { get; set; }

        [DisplayName("Alias")]
        public string Alias { get; set; }

        [DisplayName("Number of reservations")]
        public int NumberOfReservations { get { return Reservations.Count; } }

        public WasherService Service { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}