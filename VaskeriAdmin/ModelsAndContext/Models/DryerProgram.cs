using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAndContext.Models
{
    public class DryerProgram : MachineProgram
    {
        public float Price
        {
            get
            {
                float time = Length / 60f;
                return (time * ElectricityUsed);
            }
        }
    }
}
