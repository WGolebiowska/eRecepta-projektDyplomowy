using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRecepta_projektDyplomowy.ViewModels
{
    public class IllnessVm
    {
        public int IllnessId { get; set; }
        public string Name { get; set; }
        public virtual List<MedicineVm> Medicines { get; set; }
    }
}