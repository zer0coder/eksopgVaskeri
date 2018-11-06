using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAndContext.Models
{
    public class MachineProgram
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Length { get; set; }
        public float ElectricityUsed { get; set; }
    }
}
