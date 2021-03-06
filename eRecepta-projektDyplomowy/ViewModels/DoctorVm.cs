using AutoMapper.Configuration.Conventions;

namespace eRecepta_projektDyplomowy.ViewModels
{
    public class DoctorVm
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Specialty { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string FullTitle { get; set; }
    }
}
