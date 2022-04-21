using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRecepta_projektDyplomowy.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public virtual List<Appointment> Appointments { get; set; }
        public virtual List<Prescription> Prescriptions { get; set; }
        public double Amount { get; set; }
        public DateTime OrderTime { get; set; }
        public virtual List<Payment> Payment { get; set; }
        public virtual Patient Patient { get; set; }
        public string PatientId { get; set; }
        public string Status { get; set; }
    }
}
