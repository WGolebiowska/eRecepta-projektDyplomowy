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
            //CreateMap<Patient, PatientVm>().ReverseMap();
            //CreateMap<AddOrUpdatePatientVm, Patient>().ReverseMap();
            CreateMap<Doctor, DoctorVm>().ReverseMap();
            CreateMap<AddOrUpdateDoctorVm, Doctor>().ReverseMap();

            CreateMap<ApplicationUser, UserModel>()
                    .ForMember(dest => dest.Password, opt => opt.Ignore())
                    .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore());

            // Make sure to not ovewrite automatically created ApplicationUser Id when ApplicationUserViewModel Id is null
            CreateMap<UserModel, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Id, opt => opt.Condition(cond => cond.Id != null));

            CreateMap<ApplicationUser, UpdateUserModel>();

            CreateMap<UpdateUserModel, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            //CreateMap<ApplicationUser, RegisterModel>();

            //CreateMap<RegisterModel, ApplicationUser>()
            //	.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }
}