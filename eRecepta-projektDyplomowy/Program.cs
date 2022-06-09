using eRecepta_projektDyplomowy.Controllers.Services.Helpers;
using eRecepta_projektDyplomowy.Controllers.Services.Interfaces;
using eRecepta_projektDyplomowy.Data;
using eRecepta_projektDyplomowy.Models;
using eRecepta_projektDyplomowy.Services.Helpers;
using eRecepta_projektDyplomowy.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            
            string[] illnesses = { "Depresja", "Alergia", "Atopowe zapalenie skóry", "£ysienie", "Ból krêgos³upa", "Ból ucha", "Tr¹dzik", "Zapalenie gard³a", "Nadciœnienie", "Nudnoœci i wymioty" };

            IIllnessService illService = services.GetRequiredService<IIllnessService>();
            var illnessCount = illService.GetAllIllnessesCount();
            if(illnessCount == 0)
            {
                foreach(string illness in illnesses)
                {
                    var illnessEntity = new AddOrUpdateIllnessVm
                    {
                        Name = illness
                    };
                    try
                    {
                        illService.AddOrUpdateIllness(illnessEntity);
                    }
                    catch(Exception ex)
                    {
                        Debug.WriteLine($"{ex.Message}, {ex}");
                    }
                }
            }
            IMedicineService medService = services.GetRequiredService<IMedicineService>();
            var medCount = medService.GetAllMedicinesCount();
            Debug.WriteLine($"medcount: {medCount}");
            if (medCount == 0)
            {
                var i1 = illService.GetIllness(i => i.Name == "Depresja");
                    var m1 = new AddOrUpdateMedicineVm
                    {
                        Name = "Hydroxyzinum VP",
                        Form = "tabletki powlekane",
                        Dosage = "2 x dziennie po 1 tabletce",
                        ReceiptValidPeriod = 30,
                        IllnessId = i1.IllnessId

                    };
                    try
                    {
                        medService.AddOrUpdateMedicine(m1);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{ex.Message}, {ex}");
                    }
                var m2 = new AddOrUpdateMedicineVm
                {
                    Name = "Dulsevia",
                    Form = "kapsu³ki dojelitowe",
                    Dosage = "3 x dziennie po 1 kapsu³ce",
                    ReceiptValidPeriod = 30,
                    IllnessId = i1.IllnessId

                };
                try
                {
                    medService.AddOrUpdateMedicine(m2);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{ex.Message}, {ex}");
                }
                var m3 = new AddOrUpdateMedicineVm
                {
                    Name = "Mirtor",
                    Form = "tabletki ulegaj¹ce rozpadowi w jamie ustnej",
                    Dosage = "2 x dziennie po 1 tabletce",
                    ReceiptValidPeriod = 30,
                    IllnessId = i1.IllnessId

                };
                try
                {
                    medService.AddOrUpdateMedicine(m3);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{ex.Message}, {ex}");
                }
                var i2 = illService.GetIllness(i => i.Name == "Alergia");
                var mm1 = new AddOrUpdateMedicineVm
                {
                    Name = "Clatra",
                    Form = "tabletki",
                    Dosage = "2 x dziennie po 1 tabletce",
                    ReceiptValidPeriod = 30,
                    IllnessId = i2.IllnessId

                };
                try
                {
                    medService.AddOrUpdateMedicine(mm1);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{ex.Message}, {ex}");
                }
                var mm2 = new AddOrUpdateMedicineVm
                {
                    Name = "Berotec N 100",
                    Form = "aerozol inhalacyjny, roztwór",
                    Dosage = "W razie potrzeby, 1 - 2 rozpylenia przed wysi³kiem fizycznym, maksymalnie do 8 dawek aerozolu na dobê",
                    ReceiptValidPeriod = 30,
                    IllnessId = i2.IllnessId

                };
                try
                {
                    medService.AddOrUpdateMedicine(mm2);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{ex.Message}, {ex}");
                }
                var mm3 = new AddOrUpdateMedicineVm
                {
                    Name = "Zyrtec",
                    Form = "tabletki powlekane",
                    Dosage = "2 x dziennie po 1 tabletce",
                    ReceiptValidPeriod = 30,
                    IllnessId = i2.IllnessId

                };
                try
                {
                    medService.AddOrUpdateMedicine(mm3);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{ex.Message}, {ex}");
                }
                var i3 = illService.GetIllness(i => i.Name == "Atopowe zapalenie skóry");
                var mmm1 = new AddOrUpdateMedicineVm
                {
                    Name = "Elocom",
                    Form = "maœæ",
                    Dosage = "cienk¹ warstwê maœci nanosiæ na chorobowo zmienione miejsca na skórze 1 raz na dobê",
                    ReceiptValidPeriod = 30,
                    IllnessId = i3.IllnessId

                };
                try
                {
                    medService.AddOrUpdateMedicine(mmm1);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{ex.Message}, {ex}");
                }
                var mmm2 = new AddOrUpdateMedicineVm
                {
                    Name = "Dermovate",
                    Form = "roztwór na skórê",
                    Dosage = "kilka kropel p³ynu nanosiæ na chorobowo zmienione miejsca na skórze 1 raz na dobê",
                    ReceiptValidPeriod = 30,
                    IllnessId = i3.IllnessId

                };
                try
                {
                    medService.AddOrUpdateMedicine(mmm2);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{ex.Message}, {ex}");
                }
                var mmm3 = new AddOrUpdateMedicineVm
                {
                    Name = "Elidel",
                    Form = "krem",
                    Dosage = "cienk¹ warstwê kremu nanosiæ na chorobowo zmienione miejsca na skórze 1 raz na dobê",
                    ReceiptValidPeriod = 30,
                    IllnessId = i3.IllnessId

                };
                try
                {
                    medService.AddOrUpdateMedicine(mmm3);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{ex.Message}, {ex}");
                }
                var i4 = illService.GetIllness(i => i.Name == "£ysienie");
                var mmmm1 = new AddOrUpdateMedicineVm
                {
                    Name = "Nezyr",
                    Form = "tabletki powlekane",
                    Dosage = "1 x dziennie po 1 tabletce",
                    ReceiptValidPeriod = 30,
                    IllnessId = i4.IllnessId

                };
                try
                {
                    medService.AddOrUpdateMedicine(mmmm1);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{ex.Message}, {ex}");
                }
                var mmmm2 = new AddOrUpdateMedicineVm
                {
                    Name = "Minovivax 5%",
                    Form = "roztwór na skórê",
                    Dosage = "roztwór nanosiæ na skórê g³owy 2 razy na dobê",
                    ReceiptValidPeriod = 30,
                    IllnessId = i4.IllnessId

                };
                try
                {
                    medService.AddOrUpdateMedicine(mmmm2);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{ex.Message}, {ex}");
                }
                var mmmm3 = new AddOrUpdateMedicineVm
                {
                    Name = "Alpicort",
                    Form = "krem",
                    Dosage = "cienk¹ warstwê kremu nanosiæ na chorobowo zmienione miejsca na skórze 1 raz na dobê",
                    ReceiptValidPeriod = 30,
                    IllnessId = i4.IllnessId

                };
                try
                {
                    medService.AddOrUpdateMedicine(mmmm3);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{ex.Message}, {ex}");
                }
                var i5 = illService.GetIllness(i => i.Name == "Ból krêgos³upa");
                var mmmmm1 = new AddOrUpdateMedicineVm
                {
                    Name = "Doreta",
                    Form = "tabletki powlekane",
                    Dosage = "stosowaæ w razie potrzeby, maksymalnie 3 tabletki na dobê",
                    ReceiptValidPeriod = 30,
                    IllnessId = i5.IllnessId

                };
                try
                {
                    medService.AddOrUpdateMedicine(mmmmm1);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{ex.Message}, {ex}");
                }
                var mmmmm2 = new AddOrUpdateMedicineVm
                {
                    Name = "Nimesil",
                    Form = "granulat do sporz¹dzania zawiesiny doustnej",
                    Dosage = "stosowaæ w razie potrzeby, maksymalnie 3 zawiesiny na dobê",
                    ReceiptValidPeriod = 30,
                    IllnessId = i5.IllnessId

                };
                try
                {
                    medService.AddOrUpdateMedicine(mmmmm2);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{ex.Message}, {ex}");
                }
                var mmmmm3 = new AddOrUpdateMedicineVm
                {
                    Name = "DicloDuo",
                    Form = "kapsu³ki o zmodyfikowanym uwalnianiu",
                    Dosage = "stosowaæ w razie potrzeby, maksymalnie 3 tabletki na dobê",
                    ReceiptValidPeriod = 30,
                    IllnessId = i5.IllnessId

                };
                try
                {
                    medService.AddOrUpdateMedicine(mmmmm3);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{ex.Message}, {ex}");
                }
                var ii1 = illService.GetIllness(i => i.Name == "Ból ucha");
                var n1 = new AddOrUpdateMedicineVm
                {
                    Name = "Duomox",
                    Form = "tabletki",
                    Dosage = "2 x dziennie po 1 tabletce",
                    ReceiptValidPeriod = 7,
                    IllnessId = ii1.IllnessId

                };
                try
                {
                    medService.AddOrUpdateMedicine(n1);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{ex.Message}, {ex}");
                }
                var n2 = new AddOrUpdateMedicineVm
                {
                    Name = "Dicortineff",
                    Form = "krople do oczu i uszu, zawiesina",
                    Dosage = "3 x dziennie po 3 krople do bol¹cego ucha",
                    ReceiptValidPeriod = 7,
                    IllnessId = ii1.IllnessId

                };
                try
                {
                    medService.AddOrUpdateMedicine(n2);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{ex.Message}, {ex}");
                }
                var n3 = new AddOrUpdateMedicineVm
                {
                    Name = "Refastin",
                    Form = "tabletki powlekane",
                    Dosage = "2 x dziennie po 1 tabletce",
                    ReceiptValidPeriod = 30,
                    IllnessId = ii1.IllnessId

                };
                try
                {
                    medService.AddOrUpdateMedicine(n3);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{ex.Message}, {ex}");
                }
                var ii2 = illService.GetIllness(i => i.Name == "Tr¹dzik");
                var nn1 = new AddOrUpdateMedicineVm
                {
                    Name = "Duac",
                    Form = "¿el",
                    Dosage = "cienk¹ warstwê ¿elu nanosiæ na skórê twarzy 2 razy na dobê",
                    ReceiptValidPeriod = 7,
                    IllnessId = ii2.IllnessId

                };
                try
                {
                    medService.AddOrUpdateMedicine(nn1);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{ex.Message}, {ex}");
                }
                var nn2 = new AddOrUpdateMedicineVm
                {
                    Name = "Differin",
                    Form = "krem",
                    Dosage = "cienk¹ warstwê kremu nanosiæ na skórê twarzy 2 razy na dobê",
                    ReceiptValidPeriod = 30,
                    IllnessId = ii2.IllnessId

                };
                try
                {
                    medService.AddOrUpdateMedicine(nn2);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{ex.Message}, {ex}");
                }
                var nn3 = new AddOrUpdateMedicineVm
                {
                    Name = "Dalacin T",
                    Form = "emulsja na skórê",
                    Dosage = "cienk¹ warstwê emulsji nanosiæ na skórê twarzy 2 razy na dobê",
                    ReceiptValidPeriod = 7,
                    IllnessId = ii2.IllnessId

                };
                try
                {
                    medService.AddOrUpdateMedicine(nn3);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{ex.Message}, {ex}");
                }
                var ii3 = illService.GetIllness(i => i.Name == "Zapalenie gard³a");
                var nnn1 = new AddOrUpdateMedicineVm
                {
                    Name = "Augmentin",
                    Form = "tabletki powlekane",
                    Dosage = "3 x dziennie po 1 tabletce",
                    ReceiptValidPeriod = 7,
                    IllnessId = ii3.IllnessId

                };
                try
                {
                    medService.AddOrUpdateMedicine(nnn1);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{ex.Message}, {ex}");
                }
                var nnn2 = new AddOrUpdateMedicineVm
                {
                    Name = "Octeangin",
                    Form = "pastylki twarde",
                    Dosage = "Tabletkê nale¿y powoli rozpuszczaæ w jamie ustnej. Kolejne dawki przyjmowaæ co 2-3 godziny. Maksymalnie 6 tabletek dziennie",
                    ReceiptValidPeriod = 30,
                    IllnessId = ii3.IllnessId

                };
                try
                {
                    medService.AddOrUpdateMedicine(nnn2);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{ex.Message}, {ex}");
                }
                var nnn3 = new AddOrUpdateMedicineVm
                {
                    Name = "Dalacin C",
                    Form = "kapsu³ki",
                    Dosage = "4 x dziennie po 1 kapsu³ce",
                    ReceiptValidPeriod = 7,
                    IllnessId = ii3.IllnessId

                };
                try
                {
                    medService.AddOrUpdateMedicine(nnn3);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{ex.Message}, {ex}");
                }
                var ii4 = illService.GetIllness(i => i.Name == "Nadciœnienie");
                var nnnn1 = new AddOrUpdateMedicineVm
                {
                    Name = "Polsart",
                    Form = "tabletki",
                    Dosage = "1 x dziennie po 1 tabletce",
                    ReceiptValidPeriod = 30,
                    IllnessId = ii4.IllnessId

                };
                try
                {
                    medService.AddOrUpdateMedicine(nnnn1);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{ex.Message}, {ex}");
                }
                var nnnn2 = new AddOrUpdateMedicineVm
                {
                    Name = "Concor 5",
                    Form = "tabletki powlekane",
                    Dosage = "2 x dziennie po 1 tabletce",
                    ReceiptValidPeriod = 30,
                    IllnessId = ii4.IllnessId

                };
                try
                {
                    medService.AddOrUpdateMedicine(nnnn2);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{ex.Message}, {ex}");
                }
                var nnnn3 = new AddOrUpdateMedicineVm
                {
                    Name = "Ramve 5 mg",
                    Form = "kapsu³ki twarde",
                    Dosage = "1 x dziennie po 1 kapsu³ce",
                    ReceiptValidPeriod = 30,
                    IllnessId = ii4.IllnessId

                };
                try
                {
                    medService.AddOrUpdateMedicine(nnnn3);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{ex.Message}, {ex}");
                }
                var ii5 = illService.GetIllness(i => i.Name == "Nudnoœci i wymioty");
                var nnnnn1 = new AddOrUpdateMedicineVm
                {
                    Name = "Torecan",
                    Form = "tabletki powlekane",
                    Dosage = "2 x dziennie po 1 tabletce",
                    ReceiptValidPeriod = 30,
                    IllnessId = ii5.IllnessId

                };
                try
                {
                    medService.AddOrUpdateMedicine(nnnnn1);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{ex.Message}, {ex}");
                }
                var nnnnn2 = new AddOrUpdateMedicineVm
                {
                    Name = "Zofran",
                    Form = "czopki",
                    Dosage = "3 x dziennie po 1 czopku",
                    ReceiptValidPeriod = 30,
                    IllnessId = ii5.IllnessId

                };
                try
                {
                    medService.AddOrUpdateMedicine(nnnnn2);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{ex.Message}, {ex}");
                }
                var nnnnn3 = new AddOrUpdateMedicineVm
                {
                    Name = "Atossa",
                    Form = "tabletki powlekane",
                    Dosage = "2 x dziennie po 1 tabletce",
                    ReceiptValidPeriod = 30,
                    IllnessId = ii5.IllnessId

                };
                try
                {
                    medService.AddOrUpdateMedicine(nnnnn3);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"{ex.Message}, {ex}");
                }
            }
        }
    }
}