using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRecepta_projektDyplomowy.Models
{
    public class Doctor :ApplicationUser
    {
        public string MedicalDegree { get; set; }
        public string Specialty { get; set; }
        public virtual List<Appointment> Appointments { get; set; }
        public virtual List<Prescription> Prescriptions { get; set; }
    }
}