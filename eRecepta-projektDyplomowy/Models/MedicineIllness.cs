using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRecepta_projektDyplomowy.Models
{
    public class MedicineIllness
    {
        [Key]
        public int MedicineIllnessId { get; set; }

        public virtual List<Prescription> Prescription { get; set; }
        public virtual List<Medicine> Medicine { get; set; }
        public virtual List<Illness> Illness { get; set; }
    }
}