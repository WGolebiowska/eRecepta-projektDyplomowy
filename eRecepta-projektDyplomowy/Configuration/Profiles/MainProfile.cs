using AutoMapper;
using eRecepta_projektDyplomowy.Models;
using eRecepta_projektDyplomowy.ViewModels;

namespace eRecepta_projektDyplomowy.Configuration.Profiles
{
    public class MainProfile : Profile
    {
        public MainProfile()
        {
            //AutoMapper maps
            CreateMap<Appointment, AppointmentVm>().ReverseMap();
            CreateMap<AddOrUpdateAppointmentVm, Appointment>().ReverseMap();
            CreateMap<Prescription, PrescriptionVm>().ReverseMap();
            CreateMap<AddOrUpdatePrescriptionVm, Prescription>().ReverseMap();
            CreateMap<Patient, PatientVm>().ReverseMap();
            CreateMap<Doctor, DoctorVm>().ReverseMap();

            CreateMap<Doctor, UserModel>().ReverseMap();
            CreateMap<Patient, UserModel>().ReverseMap();

            CreateMap<ApplicationUser, UserModel>()
                    .Include<Doctor, UserModel>()
                    .Include<Patient, UserModel>()
                    .ForMember(dest => dest.Password, opt => opt.Ignore())
                    .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore());

            // Make sure to not ovewrite automatically created ApplicationUser Id when ApplicationUserViewModel Id is null
            CreateMap<UserModel, ApplicationUser>()
                .Include<UserModel, Doctor>()
                .Include<UserModel, Patient>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Id, opt => opt.Condition(cond => cond.Id != null));

            CreateMap<UpdateUserModel, Doctor>().ReverseMap();
            CreateMap<UpdateUserModel, Patient>().ReverseMap();

            CreateMap<ApplicationUser, UpdateUserModel>()
                .Include<Doctor, UpdateUserModel>()
                .Include<Patient, UpdateUserModel>();

            CreateMap<UpdateUserModel, ApplicationUser>()
                .Include<UpdateUserModel, Doctor>()
                .Include<UpdateUserModel, Patient>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            //CreateMap<ApplicationUser, RegisterModel>();

            //CreateMap<RegisterModel, ApplicationUser>()
            //	.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}