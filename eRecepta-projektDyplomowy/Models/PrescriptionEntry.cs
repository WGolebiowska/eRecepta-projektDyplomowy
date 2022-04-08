using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRecepta_projektDyplomowy.Models
{
    public class PrescriptionEntry
    {
        //[Key]
        public int Id { get; set; }

        public Prescription Prescription { get; set; }
        public Medicine Medicine { get; set; }
        public int Quantity { get; set; }
    }
}
