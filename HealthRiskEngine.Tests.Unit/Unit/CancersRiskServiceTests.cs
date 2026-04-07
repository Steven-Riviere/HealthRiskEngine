using CancersAPI.Services;
using Shared.Models;


namespace HealthRiskEngine.Tests.Unit
{
    public class CancersRiskServiceTests
    {
        private readonly CancersRiskService _service;

        public CancersRiskServiceTests()
        {
            _service = new CancersRiskService();
        }

        [Fact]
        public void AssessCancersRisk_ShouldReturnNone_WhenNoRiskFactors()
        {
            // Arrange
            var patient = new PatientDto { Id = 1, DateOfBirth = DateTime.Now.AddYears(-30) };
            var notes = new List<NoteDto>
            {
                new NoteDto { Notes = "Patient avec aucun symptomes" }
            };
            // Act
            var result = _service.AssessRisk(patient, notes);

            // Assert
            Assert.Equal("None", result.RiskLevel);
        }

        [Fact]
        public void AssessCancersRisk_ShouldReturnEarlyOnset_WhenMultipleRiskFactors()
        {
            // Arrange
            var patient = new PatientDto { Id = 2, DateOfBirth = DateTime.Now.AddYears(-50) };
            var notes = new List<NoteDto>
            {
                new NoteDto { Notes = "Patient avec antécédent familial de cancer et biopsie positive" }
            };
            // Act
            var result = _service.AssessRisk(patient, notes);
            // Assert
            Assert.Equal("Early Onset", result.RiskLevel);
        }
    }
}
