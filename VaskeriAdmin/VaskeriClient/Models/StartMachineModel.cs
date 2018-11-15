using ModelsAndContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VaskeriClient.Models
{
    public class StartMachineModel
    {
        public List<Machine> WashingMachines { get; set; }
        public List<Machine> DryerMachines { get; set; }
        public List<WashingProgram> WashingPrograms { get; set; }
        public List<DryerProgram> DryerPrograms { get; set; }
    }
}