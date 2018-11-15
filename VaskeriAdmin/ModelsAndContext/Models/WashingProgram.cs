using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAndContext.Models
{
    public class WashingProgram : MachineProgram
    {
        [Required]
        [DisplayName("Water used pr hour")]
        public float WaterUsed { get; set; }

        public float Price { get
            {
                float time = Length / 60f;
                return (time * WaterUsed) + (time * ElectricityUsed);
            }
        }
    }
}
