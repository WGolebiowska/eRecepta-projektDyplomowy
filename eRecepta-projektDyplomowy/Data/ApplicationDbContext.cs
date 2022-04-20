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
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Form> Forms { get; set; }
        public virtual DbSet<Illness> Illnesses { get; set; }
        public virtual DbSet<Medicine> Medicines { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Prescription> Prescriptions { get; set; }
        public virtual DbSet<PrescriptionEntry> PrescriptionEntries { get; set; }

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
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
        }
    }
}
