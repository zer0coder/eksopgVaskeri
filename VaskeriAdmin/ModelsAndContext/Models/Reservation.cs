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
        [DisplayName("Machine")]
        public Machine Machine { get; set; }

        [Required]
        [DisplayName("User")]
        public User User { get; set; }

        [Required]
        [DisplayName("Date")]
        public DateTime Date { get; set; }

        [Required]
        [DisplayName("Time")]
        public WashTime Time { get; set; }

        [Required]
        [DisplayName("Current program")]
        public MachineProgram SelectedProgram
        {
            get
            {
                switch (Machine.Type)
                {
                    case MachineType.WASHER:
                        return WashingProg;
                    case MachineType.DRYER:
                        return DryerProg;
                    default:
                        return new MachineProgram();
                }
            }
        }

        public DryerProgram DryerProg { private get; set; }
        public WashingProgram WashingProg { private get; set; }
    }
}
