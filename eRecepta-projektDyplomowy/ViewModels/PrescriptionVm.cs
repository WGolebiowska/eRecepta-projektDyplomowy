using System;

namespace eRecepta_projektDyplomowy.ViewModels
{
    public class PrescriptionVm
    {
        public int PrescriptionId { get; set; }
        public DateTime IssueDate { get; set; }
        public int ValidPeriod { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string DoctorId { get; set; }
        public DoctorVm Doctor { get; set; }
        public string PatientId { get; set; }
        public PatientVm Patient { get; set; }
        //public virtual List<PrescriptionEntry> PrescriptionEntries { get; set; }
        public int MedicineId { get; set; }
        public MedicineVm Medicine { get; set; }
        public string PrescribedDosage { get; set; }
        //public virtual Order Order { get; set; }
        //public int OrderId { get; set; }
        public string PinCode { get; set; }
        public bool Valid { get; set; }
        public string PrescriptionNotes { get; set; }
    }
}
