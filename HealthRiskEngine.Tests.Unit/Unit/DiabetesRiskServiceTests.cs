using DiabetesAPI.Services;
using Shared.Models;

namespace HealthRiskEngine.Tests.Unit
{
    public class DiabetesRiskServiceTests
    {
        private readonly DiabetesRiskService _service;
        public DiabetesRiskServiceTests()
        {
            _service = new DiabetesRiskService();
        }

        [Fact]
        public void AssessDiabetesRisk_ShouldReturnNone_WhenNoRiskFactors()
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
        public void AssessDiabetesRisk_ShouldReturnEarlyOnset_WhenMultipleRiskFactors()
        {
            // Arrange
            var patient = new PatientDto { Id = 2, DateOfBirth = DateTime.Now.AddYears(-50) };
            var notes = new List<NoteDto>
            {
                new NoteDto { Notes = "Patient avec Hémoglobine A1C élevée, Cholestérol élevé et Fumeur" }
            };
            // Act
            var result = _service.AssessRisk(patient, notes);
            // Assert
            Assert.Equal("Early Onset", result.RiskLevel);
        }
    }
}
