using System.Collections.Generic;

namespace eRecepta_projektDyplomowy.Models
{
    public class Illness
    {
        public int IllnessId { get; set; }
        public string Name { get; set; }
        public virtual List<Medicine> Medicines { get; set; }
        public virtual List<PrescriptionForm> PrescriptionForms { get; set; }
    }
}