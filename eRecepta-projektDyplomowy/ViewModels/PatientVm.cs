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
        public int Age
        {
            get
            {
                var today = DateTime.Today;

                var a = (today.Year * 100 + today.Month) * 100 + today.Day;
                var b = (DateOfBirth.Year * 100 + DateOfBirth.Month) * 100 + DateOfBirth.Day;

                return (a - b) / 10000;
            }
        }

    }
}
