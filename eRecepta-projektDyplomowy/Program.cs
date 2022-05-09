using eRecepta_projektDyplomowy.Controllers.Services.Helpers;
using eRecepta_projektDyplomowy.Controllers.Services.Interfaces;
using eRecepta_projektDyplomowy.Data;
using eRecepta_projektDyplomowy.Models;
using eRecepta_projektDyplomowy.Services.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using static eRecepta_projektDyplomowy.Services.Helpers.RoleHelpers;

namespace eRecepta_projektDyplomowy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostBuilder = CreateHostBuilder(args).ConfigureAppConfiguration((hostContext, builder) =>
            {
                builder.AddUserSecrets<Program>();
            });
            var host = hostBuilder.Build();
            InitializeDatabase(host);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        private static void InitializeDatabase(IHost host)
        {
            using var serviceScope = host.Services.CreateScope();
            var services = serviceScope.ServiceProvider;

            if (!services.GetService<ApplicationDbContext>().AllMigrationsApplied())
            {
                services.GetService<ApplicationDbContext>().Database.Migrate();
            }

            IUserManagementService umService = services.GetRequiredService<IUserManagementService>();
            var usersCount = umService.GetAllUsersCountAsync("").Result;
            if (usersCount == 0)
            {
                RoleManager<IdentityRole> roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                foreach (RolePair role in RoleHelpers.Roles)
                {
                    if (!roleManager.RoleExistsAsync(role.Name).Result)
                    {
                        var idRole = new IdentityRole(role.Name);
                        roleManager.CreateAsync(idRole).Wait();
                    }
                }

                // Create admin user
                ApplicationUser adminUser = new ApplicationUser
                {
                    UserName = "admin@erecepta.com",
                    Email = "admin@erecepta.com",
                    Name = "AdminImie",
                    Surname = "AdminNazwisko",
                    PESEL = "97090978900",
                    EmailConfirmed = true,
                    Approved = true
                };

                umService.AddUserAsync(adminUser, "ZAQ!2wsx", "administrator").Wait();

                UserManager<ApplicationUser> userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                List<Doctor> doctorUsers = new List<Doctor>();
                
                var doc1 = new Doctor 
                {
                    UserName = "doctor1@erecepta.com",
                    Email = "doctor1@erecepta.com",
                    Name = "Gregory",
                    Surname = "House",
                    PESEL = "80020299999",
                    PhoneNumber = "662111991",
                    Specialty = "pediatra",
                    EmailConfirmed = true,
                    Approved = true
                };
                doctorUsers.Add(doc1);
                var doc2 = new Doctor
                {
                    UserName = "doctor2@erecepta.com",
                    Email = "doctor2@erecepta.com",
                    Name = "George",
                    Surname = "Clooooney",
                    PESEL = "70030399999",
                    PhoneNumber = "662222991",
                    Specialty = "internista",
                    EmailConfirmed = true,
                    Approved = true
                };
                doctorUsers.Add(doc2);

                var doc3 = new Doctor
                {
                    UserName = "doctor3@erecepta.com",
                    Email = "doctor3@erecepta.com",
                    Name = "Artur",
                    Surname = "Zmijewski",
                    PESEL = "60040499999",
                    PhoneNumber = "662333991",
                    Specialty = "psychiatra",
                    EmailConfirmed = true,
                    Approved = true
                };
                doctorUsers.Add(doc3);

                foreach(Doctor doctor in doctorUsers)
                {
                    umService.AddUserAsync(doctor, "ZAQ!2wsx", "doctor").Wait();
                }

                ApplicationUser patientUser = new Patient();
                for (int i = 1; i < 125; i++)
                {
                    patientUser = new Patient
                    {
                        UserName = "user" + i + "@erecepta.com",
                        Email = "user" + i + "@erecepta.com",
                        Name = "USER" + i + "IMIE",
                        Surname = "USER" + i + "NAZWISKO",
                        PESEL = "90010155533" ,
                        PhoneNumber = "662555991",
                        EmailConfirmed = true,
                        Approved = true
                    };
                    umService.AddUserAsync(patientUser, "ZAQ!2wsx", "patient").Wait();
                }
            }
        }
    }
}