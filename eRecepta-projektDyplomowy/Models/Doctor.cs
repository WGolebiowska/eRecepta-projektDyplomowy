using System.Collections.Generic;

namespace eRecepta_projektDyplomowy.Models
{
    public class Doctor : ApplicationUser
    {
        public string MedicalDegree { get; set; }
        public string Specialty { get; set; }
        public virtual List<Appointment> Appointments { get; set; }
        public virtual List<Prescription> Prescriptions { get; set; }
    }
}