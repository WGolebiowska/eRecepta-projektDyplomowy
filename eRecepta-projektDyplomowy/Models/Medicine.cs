using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRecepta_projektDyplomowy.Models
{
    public class Medicine
    {
        // [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public virtual List<Illness> Illnesses { get; set; }
    }
}
