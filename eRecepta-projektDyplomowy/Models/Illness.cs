using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRecepta_projektDyplomowy.Models
{
    public class Illness
    {
       // [Key]
        public int Id { get; set; }
       
        public string Name { get; set; }
        public virtual List<Medicine> Medicines { get; set; }
    }
}
