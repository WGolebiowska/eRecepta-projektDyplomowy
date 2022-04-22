using AutoMapper;
using eRecepta_projektDyplomowy.Models;
using eRecepta_projektDyplomowy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRecepta_projektDyplomowy.Configuration.Profiles
{
    public class MainProfile : Profile
    {
        public MainProfile()
        {
            //AutoMapper maps
            CreateMap<Appointment, AppointmentVm>().ReverseMap();
            CreateMap<AddOrUpdateAppointmentVm, Appointment>().ReverseMap();
            //CreateMap<Patient, PatientVm>().ReverseMap();
            //CreateMap<AddOrUpdatePatientVm, Patient>().ReverseMap();
            CreateMap<Doctor, DoctorVm>().ReverseMap();
            CreateMap<AddOrUpdateDoctorVm, Doctor>().ReverseMap();
        }
    }
}