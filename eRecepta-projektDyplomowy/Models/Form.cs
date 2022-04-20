using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRecepta_projektDyplomowy.Models
{
    public class Form
    {
        [Key]
        public int FormId { get; set; }
       
        public string Type { get; set; }
        public Medicine Medicine { get; set; }
        public virtual List<string> Questions { get; set; }
    }

}
