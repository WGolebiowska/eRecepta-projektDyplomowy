using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRecepta_projektDyplomowy.Models
{
    public class Prescription
    {

        //[Key]
        public int Id { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Doctor Issuer { get; set; }
        public Patient Patient { get; set; }
        public virtual List<Prescription> PrescriptionEntry { get; set; }
        public string PinCode { get; set; }
        public Order Order { get; set; }
        public bool Valid { get; set; }
        public string PrescriptionNotes { get; set; }



    }
}
