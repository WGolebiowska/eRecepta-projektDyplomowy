using eRecepta_projektDyplomowy.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRecepta_projektDyplomowy.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
<<<<<<< Updated upstream
=======
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                    .ToTable("AspNetUsers")
                    .HasDiscriminator<int>("UserType")
                    .HasValue<ApplicationUser>(0)
                    .HasValue<Doctor>(1)
                    .HasValue<Patient>(2);

            modelBuilder.Entity<Order>()
                    .HasMany(ord => ord.Payment)
                    .WithOne(pay => pay.Order)
                    .HasForeignKey(ord => ord.PaymentId)
                    .IsRequired();

            modelBuilder.Entity<Order>()
                    .HasMany(ord => ord.Prescriptions)
                    .WithOne(pre => pre.Order)
                    .HasForeignKey(ord => ord.PrescriptionId)
                    .IsRequired();

            modelBuilder.Entity<Order>()
                    .HasMany(ord => ord.Appointments)
                    .WithOne(app => app.Order)
                    .HasForeignKey(ord => ord.AppointmentId)
                    .IsRequired();

            modelBuilder.Entity<Patient>()
                    .HasMany(pat => pat.Orders)
                    .WithOne(ord => ord.Client)
                    .HasForeignKey(pat => pat.OrderId)
                    .IsRequired();

            modelBuilder.Entity<Patient>()
                    .HasMany(pat => pat.Appointments)
                    .WithOne(app => app.Patient)             // raz jest Patient, raz Client, moze ujednolicic?
                    .HasForeignKey(pat => pat.AppointmentId)
                    .IsRequired();

            modelBuilder.Entity<Patient>()
                    .HasMany(pat => pat.Prescriptions)
                    .WithOne(pre => pre.Patient)
                    .HasForeignKey(pat => pat.PrescriptionId)
                    .IsRequired();

            modelBuilder.Entity<Doctor>()
                    .HasMany(doc => doc.Appointments)
                    .WithOne(app => app.Doctor)            
                    .HasForeignKey(doc => doc.AppointmentId)
                    .IsRequired();

            modelBuilder.Entity<Doctor>()
                    .HasMany(doc => doc.Prescriptions)
                    .WithOne(pre => pre.Issuer)
                    .HasForeignKey(doc => doc.PrescriptionId)
                    .IsRequired();

            modelBuilder.Entity<Medicine>()
                    .HasOne<Form>(med => med.Form)
                    .WithOne(f => f.Medicine)
                    .HasForeignKey<Form>(med => med.FormId)
                    .IsRequired();

            // no nie wiem juz jak to poprawic
            /*
            modelBuilder.Entity<PrescriptionEntry>()
                .HasKey(pe => new { pe.PrescriptionId, pe. MedicineId });


       

            modelBuilder.Entity<PrescriptionEntry>()
                    .HasMany<Prescription>(pe => pe.Prescription)
                    .WithOne<Prescription>(pre => pre.PrescriptionEntry)
                    .HasForeignKey(pe => pe.PrescriptionId)
                    .IsRequired()
                    ;

            modelBuilder.Entity<PrescriptionEntry>()
                    .HasMany<Medicine>(pe => pe.Medicine) 
                    .WithOne<Prescription>(med => med.PrescriptionEntry) 
                    .HasForeignKey(pe => pe.MedicineId)
                    .IsRequired()
                    ;

            modelBuilder.Entity<MedicineIllness>()
                    .HasMany<Medicine>(mi => mi.Medicine)
                    .WithOne(med => med.MedicineIllness) 
                    .HasForeignKey(mi => mi.MedicineId)
                    .IsRequired();

            modelBuilder.Entity<MedicineIllness>()
                    .HasMany<Medicine>(mi => mi.Illness)
                    .WithOne(il => il.MedicineIllness)
                    .HasForeignKey(mi => mi.IlnessId)
                    .IsRequired();*/

        }
>>>>>>> Stashed changes
    }
}
