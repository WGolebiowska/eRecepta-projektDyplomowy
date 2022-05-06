using System;
using System.ComponentModel.DataAnnotations;

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
        public enum _Type { PhoneCall, VideoConference }
        [Required]
        public _Type Type { get; set; }
    }
}
