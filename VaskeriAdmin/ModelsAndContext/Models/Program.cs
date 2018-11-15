using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAndContext.Models
{
    public class MachineProgram
    {
        public int Id { get; set; }

        [Required]
        public int ServiceID { get; set; }

        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Length of program")]
        public int Length { get; set; }

        [Required]
        [DisplayName("Temperature")]
        public int Temperatur { get; set; }

        [Required]
        [DisplayName("Approx. watt usage pr. hour")]
        public float ElectricityUsed { get; set; }
    }
}
