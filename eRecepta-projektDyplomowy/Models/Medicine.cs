using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRecepta_projektDyplomowy.Models
{
    public class Medicine
    {
        [Key]
        public int MedicineId { get; set; }

        public string Name { get; set; }
        public Form Form { get; set; }
        public virtual List<Illness> Illnesses { get; set; }

        public virtual List<MedicineIllness> MedicineIllness { get; set; }

        public virtual List<Prescription> PrescriptionEntry { get; set; }
    }
}
