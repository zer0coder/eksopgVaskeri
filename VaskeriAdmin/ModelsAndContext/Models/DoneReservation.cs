using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAndContext.Models
{
    public class DoneReservation
    {
        public int Id { get; set; }

        [Required]
        public int UID { get; set; }

        [Required]
        public Reservation Reservation { get; set; }

        public float Cost
        {
            get
            {
                float cc = 0f;
                if (DryerProgs != null)
                {
                    foreach (var item in DryerProgs)
                    {
                        cc += item.Price;
                    }
                }

                if (WashingProgs != null)
                {
                    foreach (var item in WashingProgs)
                    {
                        cc += item.Price;
                    }
                }

                return cc;
            }
        }

        public List<DryerProgram> DryerProgs { get; set; }
        public List<WashingProgram> WashingProgs { get; set; }
    }
}
