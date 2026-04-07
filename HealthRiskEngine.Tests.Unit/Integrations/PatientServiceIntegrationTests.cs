using PatientsAPI.Services;
using PatientsAPI.Data;
using PatientsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthRiskEngine.Tests.Integrations
{
    public class PatientServiceIntegrationTests
    {
        private PatientsDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<PatientsDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new PatientsDbContext(options);
        }

        [Fact]
        public async Task AddPatientAsync_ShouldAddPatient()
        {
            // Arrange
            var context = GetDbContext();
            var service = new PatientService(context);
            var patient = new Patient
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new System.DateTime(1990, 1, 1),
                Gender = Shared.Models.GenderEnum.Male,
                City = "New York"
            };

            //Act
            await service.AddPatientAsync(patient);

            //Assert
            var patients = await context.Patients.ToListAsync();
            Assert.Single(patients);
            Assert.Equal("John", patients.First().FirstName);
        }

        [Fact]
        public async Task GetPatientByIdAsync_ShouldReturnPatient()
        {
            // Arrange
            var context = GetDbContext();
            var service = new PatientService(context);
            var patient = new Patient
            {
                FirstName = "Jane",
                LastName = "Smith",
                DateOfBirth = new System.DateTime(1985, 5, 15),
            };
            context.Patients.Add(patient);
            await context.SaveChangesAsync();

            //Act
            var result = await service.GetPatientByIdAsync(patient.Id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Jane", result.FirstName);


        }

        [Fact]
        public async Task UpdatePatient_ShouldModifyData()
        {
            var context = GetDbContext();
            var service = new PatientService(context);

            var patient = new Patient { FirstName = "Cynthia" };
            context.Patients.Add(patient);
            await context.SaveChangesAsync();

            patient.FirstName = "Robert";

            await service.UpdatePatientAsync(patient);

            var updated = await context.Patients.FindAsync(patient.Id);

            Assert.Equal("Robert", updated.FirstName);
        }

        [Fact]
        public async Task DeletePatient_ShouldRemovePatient()
        {
            var context = GetDbContext();
            var service = new PatientService(context);

            var patient = new Patient { FirstName = "JohnADelete" };
            context.Patients.Add(patient);
            await context.SaveChangesAsync();

            await service.DeletePatientAsync(patient.Id);

            var result = await context.Patients.FindAsync(patient.Id);

            Assert.Null(result);
        }
    }
}
