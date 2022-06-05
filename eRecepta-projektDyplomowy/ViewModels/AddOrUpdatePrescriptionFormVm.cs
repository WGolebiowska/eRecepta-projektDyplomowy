using System;
using System.ComponentModel.DataAnnotations;
using static eRecepta_projektDyplomowy.Models.Appointment;

namespace eRecepta_projektDyplomowy.ViewModels
{
    public class AddOrUpdatePrescriptionFormVm
    {
        public int? PrescriptionFormId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public string PatientId { get; set; }
        [Required]
        public int IllnessId { get; set; }
        public int? MedicineId { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public bool IsPregnant { get; set; }
        [Required]
        public string Height { get; set; }
        [Required]
        public string Weight { get; set; }
        [Required]
        public string BodyTemp { get; set; }
        public string Addictions { get; set; }
        public string Allergies { get; set; }
        public string PermMedicines { get; set; }
        public string ChronicIllnesses { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
