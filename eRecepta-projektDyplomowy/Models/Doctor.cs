using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRecepta_projektDyplomowy.Models
{
    public class Doctor : ApplicationUser
    {
        public string Specialty { get; set; }
        public virtual List<Appointment> Appointments { get; set; }
        public virtual List<Prescription> Prescriptions { get; set; }
        [NotMapped]
        public string FullTitle 
        {
            get
            {
                return Name + " " + Surname + ", lekarz " + Specialty;
            }
        }
    }
}