﻿using AutoMapper.Configuration.Conventions;

namespace eRecepta_projektDyplomowy.ViewModels
{
    public class DoctorVm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MedicalDegree { get; set; }
        public string Specialty { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
