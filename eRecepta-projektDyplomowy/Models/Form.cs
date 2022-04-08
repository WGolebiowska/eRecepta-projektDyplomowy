using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRecepta_projektDyplomowy.Models
{
    public class Form
    {
       // [Key]
        public int Id { get; set; }
       
        public string Type { get; set; }
        public Medicine Medicine { get; set; }
        public virtual List<string> Questions { get; set; }
    }

}
