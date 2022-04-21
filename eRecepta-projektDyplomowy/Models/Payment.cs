using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRecepta_projektDyplomowy.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public virtual Order Order { get; set; }
        public int OrderId { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime PaymentTime { get; set; }

    }
}
