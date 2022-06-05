using System;
using System.ComponentModel.DataAnnotations;
using static eRecepta_projektDyplomowy.Models.Appointment;

namespace eRecepta_projektDyplomowy.ViewModels
{
    public class AddOrUpdatePrescriptionVm
    {
        public int? PrescriptionId { get; set; }
        [Required]
        public DateTime IssueDate { get; set; }
        [Required]
        public string DoctorId { get; set; }
        [Required]
        public string PatientId { get; set; }
        [Required]
        public int MedicineId { get; set; }
        [Required]
        public string PrescribedDosage { get; set; }

        //public virtual List<PrescriptionEntry> PrescriptionEntries { get; set; }
        //public virtual List<Medicine> Medicines { get; set; }
        //public virtual Order Order { get; set; }
        //public int OrderId { get; set; }
        public string PrescriptionNotes { get; set; }
    }
}
