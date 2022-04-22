using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRecepta_projektDyplomowy.ViewModels
{
    public class DoctorVm
    {
        [MapTo("Id")]
        public string DoctorId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MedicalDegree { get; set; }
        public string Specialty { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
