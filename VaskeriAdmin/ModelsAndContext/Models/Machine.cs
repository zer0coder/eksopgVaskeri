using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ModelsAndContext.Models
{
    public class Machine
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Number")]
        public int Number { get; set; }

        [Required]
        [DisplayName("Machine type")]
        public MachineType Type { get; set; }

        [DisplayName("In Use")]
        public bool InUse { get; set; }

        [DisplayName("Started")]
        public DateTime? StartTime { get; set; }

        [DisplayName("Ends")]
        public DateTime? EndTime { get; set; }

        [Required]
        public int ServiceID { get; set; }

        [DisplayName("Current program")]
        public int SelectedProgram
        {
            get
            {
                switch (Type)
                {
                    case MachineType.WASHER:
                        if (WashingProg != null)
                        {
                            return WashingProg.Id;
                        }
                        else
                        {
                            return -1;
                        }
                    case MachineType.DRYER:
                        if (DryerProg != null)
                        {
                            return DryerProg.Id;
                        }
                        else
                        {
                            return -1;
                        }
                    default:
                        return -1;
                }
            }
        }

        public DryerProgram DryerProg { get; set; }
        public WashingProgram WashingProg { get; set; }


        public List<Reservation> Reservations { get; set; }
    }

    public enum MachineType
    {
        WASHER, DRYER
    }
}