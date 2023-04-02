using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Numerics;

namespace Dental_Clinic.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<MedServices> MedServices { get; set; }
        public DbSet<ServicesProvided> ServicesProvideds { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<Diagnos> Diagnos { get; set; }
        public DbSet<ListPrepforTreatment> ListPrepforTreatments { get; set; }
        public DbSet<MedTreatment> MedTreatments { get; set; }
    }
}
