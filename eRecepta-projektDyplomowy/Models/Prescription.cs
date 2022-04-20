using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRecepta_projektDyplomowy.Models
{
    public class Prescription
    {

        [Key]
        public int PrescriptionId { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Doctor Issuer { get; set; }
        public Patient Patient { get; set; }
        public Prescription PrescriptionEntry { get; set; }
        public string PinCode { get; set; }
        public Order Order { get; set; }
        public bool Valid { get; set; }
        public string PrescriptionNotes { get; set; }



    }
}
