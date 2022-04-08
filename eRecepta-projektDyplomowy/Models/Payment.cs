using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRecepta_projektDyplomowy.Models
{
    public class Payment
    {
        //[Key]
        public int Id { get; set; }

        public Order Order { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime PaymentTime { get; set; }

    }
}
