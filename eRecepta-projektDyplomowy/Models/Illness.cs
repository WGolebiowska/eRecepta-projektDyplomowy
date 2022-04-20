using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRecepta_projektDyplomowy.Models
{
    public class Illness
    {
        [Key]
        public int IllnessId { get; set; }
       
        public string Name { get; set; }
        public virtual List<Medicine> Medicines { get; set; }
        public virtual List<MedicineIllness> MedicineIllness { get; set; }
    }
}
