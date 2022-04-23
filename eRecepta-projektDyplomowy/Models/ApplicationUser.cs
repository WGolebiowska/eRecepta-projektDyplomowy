using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRecepta_projektDyplomowy.Models
{
    public class ApplicationUser : IdentityUser
    {
        public int UserType { get; set; }
        [PersonalData]
        public string Name { get; set; }
        [PersonalData]
        public string Surname { get; set; }
        [PersonalData]
        public string PESEL { get; set; }
        public bool IsInsured { get; set; }
    }
}
