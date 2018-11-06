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

        [DisplayName("Reserved")]
        public bool Reserved { get; set; }

        public WasherService Service { get; set; }
    }

    public enum MachineType
    {
        WASHER, DRYER
    }
}