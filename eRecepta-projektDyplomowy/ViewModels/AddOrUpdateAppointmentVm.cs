using System;
using System.ComponentModel.DataAnnotations;
using static eRecepta_projektDyplomowy.Models.Appointment;

namespace eRecepta_projektDyplomowy.ViewModels
{
    public class AddOrUpdateAppointmentVm
    {
        public int? AppointmentId { get; set; }
        [Required]
        public DateTime AppointmentDate { get; set; }
        [Required]
        public string DoctorId { get; set; }
        [Required]
        public string PatientId { get; set; }
        public string AppointmentNotes { get; set; }
        [Required]
        public _Type Type { get; set; }
        //[Required]
        //public int? OrderId { get; set; } 
    }
}
