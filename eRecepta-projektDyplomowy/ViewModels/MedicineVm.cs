using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRecepta_projektDyplomowy.ViewModels
{
    public class MedicineVm
    {
        public int MedicineId { get; set; }
        public string Name { get; set; }
        public string Form { get; set; }
        public string Dosage { get; set; }
        public int ReceiptValidPeriod { get; set; }
    }
}