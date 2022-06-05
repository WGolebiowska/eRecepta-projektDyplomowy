using System.Collections.Generic;

namespace eRecepta_projektDyplomowy.Models
{
    public class Medicine
    {
        public int MedicineId { get; set; }
        public string Name { get; set; }
        public string Form { get; set; }
        public string Dosage { get; set; }
        public int ReceiptValidPeriod { get; set; }
        public int IllnessId { get; set; }
        public virtual Illness Illness { get; set; }
        public virtual List<Prescription> Prescriptions { get; set; }
        public virtual List<PrescriptionForm> PrescriptionForms { get; set; }
    }
}