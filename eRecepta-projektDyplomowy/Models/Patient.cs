using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRecepta_projektDyplomowy.Models
{
    public class Patient // : User
    {
        // [Key]

        public virtual List<Appointment> Appointments { get; set; }
        public virtual List<Prescription> Prescriptions { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
