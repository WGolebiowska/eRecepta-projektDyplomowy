using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eRecepta_projektDyplomowy.ViewModels
{
    public class AddOrUpdateDoctorVm
    {
        public string Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", 
         ErrorMessage = "Niedozwolone znaki")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", 
         ErrorMessage = "Niedozwolone znaki")]
        public string Surname { get; set; }
        [Required]
        [RegularExpression(@"^[\d]{11}$", 
         ErrorMessage = "PESEL musi się składać z 11 cyfr")]
        public string PESEL { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", 
         ErrorMessage = "Niedozwolone znaki")]
        public string MedicalDegree { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", 
         ErrorMessage = "Niedozwolone znaki")]
        public string Specialty { get; set; }
    }
}
