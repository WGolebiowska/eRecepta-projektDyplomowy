using AutoMapper.Configuration.Conventions;
using System;

namespace eRecepta_projektDyplomowy.ViewModels
{
    public class PatientVm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PESEL { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }

    }
}
