using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAndContext.Models
{
    public class WashTime
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required]
        [DisplayName("From")]
        public TimeSpan Start { get; set; }

        [Required]
        [DisplayName("To")]
        public TimeSpan End { get; set; }

        public WasherService Service { get; set; }
    }
}
