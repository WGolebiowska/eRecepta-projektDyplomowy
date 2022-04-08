using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRecepta_projektDyplomowy.Models
{
    public class Order
    {

        //[Key]
        public int Id { get; set; }

        public Appointment Appointment { get; set; }
        public virtual List<Prescription> Prescriptions { get; set; }
        public double Amount { get; set; }
        public DateTime OrderTime { get; set; }
        public Payment Payment { get; set; }
        public Patient Client { get; set; }
        public string Status { get; set; }
    }
}
