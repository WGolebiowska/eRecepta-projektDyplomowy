using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRecepta_projektDyplomowy.Models
{
    public class PrescriptionForm
    {
        public int PrescriptionFormId { get; set; }
        public DateTime OrderDate { get; set; }
        public string PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        public int IllnessId { get; set; }
        public virtual Illness Illness { get; set; }
        public int? MedicineId { get; set; }
        public virtual Medicine Medicine { get; set; }
        public string Gender { get; set; }
        public bool IsPregnant { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string BodyTemp { get; set; }
        public string Addictions { get; set; }
        public string Allergies { get; set; }
        public string PermMedicines { get; set; }
        public string ChronicIllnesses { get; set; }
        public string AdditionalInfo { get; set; }
    }
}