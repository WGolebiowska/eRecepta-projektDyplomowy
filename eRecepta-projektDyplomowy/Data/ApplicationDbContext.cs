using eRecepta_projektDyplomowy.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;

namespace eRecepta_projektDyplomowy.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Illness> Illnesses { get; set; }
        public virtual DbSet<Medicine> Medicines { get; set; }
        //public virtual DbSet<Order> Orders { get; set; }
        //public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Prescription> Prescriptions { get; set; }
        //public virtual DbSet<PrescriptionEntry> PrescriptionEntries { get; set; }
        public virtual DbSet<PrescriptionForm> PrescriptionForms { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.ConfigureWarnings(warn => warn.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning));
            optionsBuilder.ConfigureWarnings(warn => warn.Ignore(CoreEventId.DetachedLazyLoadingWarning));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // modelBuilder.Entity<ApplicationUser>()
            //     .Property(e => e.Id)
            //     .ValueGeneratedOnAdd();

            modelBuilder.Entity<ApplicationUser>()
                    .ToTable("AspNetUsers")
                    .HasDiscriminator<int>("UserType")
                    .HasValue<ApplicationUser>(0)
                    .HasValue<Doctor>(1)
                    .HasValue<Patient>(2);

            //modelBuilder.Entity<Order>()
            //        .HasMany(ord => ord.Payment)
            //        .WithOne(pay => pay.Order)
            //        .HasForeignKey(pay => pay.OrderId)
            //        .IsRequired();

            //modelBuilder.Entity<Order>()
            //        .HasMany(ord => ord.Prescriptions)
            //        .WithOne(pre => pre.Order)
            //        .HasForeignKey(pre => pre.OrderId)
            //        .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Order>()
            //        .HasMany(ord => ord.Appointments)
            //        .WithOne(app => app.Order)
            //        .HasForeignKey(app => app.OrderId)
            //        .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Patient>()
            //        .HasMany(pat => pat.Orders)
            //        .WithOne(ord => ord.Patient)
            //        .HasForeignKey(ord => ord.PatientId)
            //        .IsRequired();

            modelBuilder.Entity<Patient>()
                    .HasMany(pat => pat.Appointments)
                    .WithOne(app => app.Patient)
                    .HasForeignKey(app => app.PatientId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Patient>()
                    .HasMany(pat => pat.Prescriptions)
                    .WithOne(pre => pre.Patient)
                    .HasForeignKey(pre => pre.PatientId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Patient>()
            .HasMany(pat => pat.PrescriptionForms)
            .WithOne(pre => pre.Patient)
            .HasForeignKey(pre => pre.PatientId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Medicine>()
                .HasMany(m => m.PrescriptionForms)
                .WithOne(p => p.Medicine)
                .HasForeignKey(p => p.MedicineId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Medicine>()
                .HasMany(m => m.Prescriptions)
                .WithOne(p => p.Medicine)
                .HasForeignKey(p => p.MedicineId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Illness>()
                .HasMany(i => i.PrescriptionForms)
                .WithOne(p => p.Illness)
                .HasForeignKey(p => p.IllnessId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Doctor>()
                    .HasMany(doc => doc.Appointments)
                    .WithOne(app => app.Doctor)
                    .HasForeignKey(app => app.DoctorId)
                    .IsRequired();

            modelBuilder.Entity<Doctor>()
                    .HasMany(doc => doc.Prescriptions)
                    .WithOne(pre => pre.Doctor)
                    .HasForeignKey(pre => pre.DoctorId)
                    .IsRequired();

            modelBuilder.Entity<Illness>()
                    .HasMany(i => i.Medicines)
                    .WithOne(m => m.Illness)
                    .HasForeignKey(m => m.IllnessId)
                    .IsRequired();

            //modelBuilder.Entity<PrescriptionEntry>()
            //    .HasKey(pe => new { pe.PrescriptionId, pe.MedicineId });

            //modelBuilder.Entity<PrescriptionEntry>()
            //    .HasOne(pe => pe.Prescription)
            //    .WithMany(p => p.PrescriptionEntries)
            //    .HasForeignKey(pe => pe.PrescriptionId)
            //    .IsRequired();

            //modelBuilder.Entity<PrescriptionEntry>()
            //       .HasOne(pe => pe.Medicine)
            //       .WithMany(m => m.PrescriptionEntries)
            //       .HasForeignKey(pe => pe.MedicineId)
            //       .IsRequired();

            modelBuilder.Entity<ApplicationUser>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne()
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne()
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne()
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne()
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
        }
    }
}