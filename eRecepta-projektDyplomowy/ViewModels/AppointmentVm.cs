﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRecepta_projektDyplomowy.ViewModels
{
    public class AppointmentVm
    {
        public int AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSurname { get; set; }
        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public string PatientSurname { get; set; }
        public string AppointmentNotes { get; set; }
        public string Status { get; set; }
        public enum _Type { PhoneCall, VideoConference }
        public _Type Type { get; set; }
        public string VideoConferenceURL { get; set; }
    }
}