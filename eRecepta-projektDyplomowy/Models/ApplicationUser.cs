using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eRecepta_projektDyplomowy.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
        public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
        public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; }
        public int UserType { get; set; }
        [PersonalData]
        public string Name { get; set; }
        [PersonalData]
        public string Surname { get; set; }
        [PersonalData]
        public string PESEL { get; set; }
        public bool Approved { get; set; } = false;
        public bool ApplicationEditingAllowed { get; set; } = true;
        [NotMapped]
        public string FullName
        {
            get
            {
                return Name + " " + Surname;
            }
        }
        [NotMapped]
        public DateTime DateOfBirth
        {
            get
            {
                if (PESEL == null)
                    return new DateTime(0, 0, 0);
                else
                {
                    var year = Int32.Parse(PESEL.Substring(0, 2));
                    var month = Int32.Parse(PESEL.Substring(2, 2));
                    var day = Int32.Parse(PESEL.Substring(4, 2));

                    if (month > 20)
                    {
                        year += 2000;
                        month -= 20;
                    }
                    else
                    {
                        year += 1900;
                    }
                    String Date = year.ToString() + "-" + month.ToString() + "-" + day.ToString();
                    return DateTime.Parse(Date);

                }
            }
        }
    }
}
