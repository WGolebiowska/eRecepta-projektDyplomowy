using System.Collections.Generic;

namespace eRecepta_projektDyplomowy.Models
{
    public class Patient : ApplicationUser
    {
        public virtual List<Appointment> Appointments { get; set; }
        public virtual List<Prescription> Prescriptions { get; set; }
        //public virtual List<Order> Orders { get; set; }
    }
}
