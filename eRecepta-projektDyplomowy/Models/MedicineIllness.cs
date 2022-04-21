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
        public virtual Medicine Medicine { get; set; }
        public int MedicineId { get; set; }
        public virtual Illness Illness { get; set; }
        public int IllnessId { get; set; }
    }
}