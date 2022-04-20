using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRecepta_projektDyplomowy.Models
{
    public class PrescriptionEntry
    {
        //[Key]
        //public int PrescriptionEntryId { get; set; }

        public virtual List<Prescription> Prescription { get; set; }
        public Medicine Medicine { get; set; }
        public int Quantity { get; set; }
    }
}
