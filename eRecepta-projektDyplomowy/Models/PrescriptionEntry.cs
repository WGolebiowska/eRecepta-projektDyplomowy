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
        public virtual Prescription Prescription { get; set; }
        public int PrescriptionId { get; set; }
        public virtual Medicine Medicine { get; set; }
        public int MedicineId { get; set; }
        public int Quantity { get; set; }
    }
}