using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRecepta_projektDyplomowy.Models
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }
        public DateTime IssueDate { get; set; }
        [NotMapped]
        public int ValidPeriod 
        { 
            get
            {
                if(this.Medicines != null && this.Medicines.Exists(m => m.Name.Contains("antybiotyk")))
                {
                    return 7;
                }
                else
                {
                    return 30;
                }
            }
        }
        [NotMapped]
        public DateTime ExpiryDate { 
            get
            {
                if (this.IssueDate != null)
                    return this.IssueDate.AddDays(this.ValidPeriod);
                else
                    return new DateTime();
            }
        }

        public virtual Doctor Doctor { get; set; }
        public string DoctorId { get; set; }
        public virtual Patient Patient { get; set; }
        public string PatientId { get; set; }
        public virtual List<PrescriptionEntry> PrescriptionEntries { get; set; }
        public virtual List<Medicine> Medicines { get; set; }
        public string PinCode { get; set; }
        //public virtual Order Order { get; set; }
        //public int OrderId { get; set; }
        [NotMapped]
        public bool Valid { 
            get
            {
                if ((DateTime.Compare(this.ExpiryDate, DateTime.Today)) >= 0)
                    return true;
                else
                    return false;
            }
        }
        public string PrescriptionNotes { get; set; }
    }
}