using Microsoft.EntityFrameworkCore;
using PatientsAPI.Data.SeedData;
using PatientsAPI.Models;

namespace PatientsAPI.Data;

public class PatientsDbContext : DbContext
{
    public PatientsDbContext(DbContextOptions<PatientsDbContext> options) : base(options) {}

    public DbSet<Patient> Patients { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Patient>()
            .Property(p => p.Gender)
            .HasConversion<int>();

        PatientsData.SeedPatients(modelBuilder);
    }
}
