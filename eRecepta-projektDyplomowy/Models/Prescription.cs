using System;
using System.Collections.Generic;

namespace eRecepta_projektDyplomowy.Models
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public virtual Doctor Doctor { get; set; }
        public string DoctorId { get; set; }
        public virtual Patient Patient { get; set; }
        public string PatientId { get; set; }
        public virtual List<PrescriptionEntry> PrescriptionEntries { get; set; }
        public virtual List<Medicine> Medicines { get; set; }
        public string PinCode { get; set; }
        public virtual Order Order { get; set; }
        public int OrderId { get; set; }
        public bool Valid { get; set; }
        public string PrescriptionNotes { get; set; }
    }
}