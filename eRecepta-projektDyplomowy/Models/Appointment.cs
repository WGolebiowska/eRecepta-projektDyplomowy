using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRecepta_projektDyplomowy.Models
{
    public class Appointment
    {
        //[Key]
        public int Id { get; set; }

        public DateTime AppointmentDate { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public string AppointmentNotes { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string VideoConferenceURL { get; set; }
     

    }
}
