using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRecepta_projektDyplomowy.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public virtual Doctor Doctor { get; set; }
        public string DoctorId { get; set; }
        public virtual Patient Patient { get; set; }
        public string PatientId { get; set; }
        public string AppointmentNotes { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string VideoConferenceURL { get; set; }
        public virtual Order Order { get; set; }
        public int? OrderId { get; set; }

    }
}
