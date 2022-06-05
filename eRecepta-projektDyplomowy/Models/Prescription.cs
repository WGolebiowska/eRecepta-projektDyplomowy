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
                if(this.Medicine != null)
                {
                    return this.Medicine.ReceiptValidPeriod;
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
        public int MedicineId { get; set; }
        public virtual Medicine Medicine { get; set; }
        public string PrescribedDosage { get; set; }
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