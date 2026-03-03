using DiabetesAPI.Models;
using Shared.Models;

namespace DiabetesAPI.Services
{
    public class DiabetesRiskService : IDiabetesRiskService
    {
        public DiabetesRisk AssessRisk(PatientDto patient, List<NoteDto> notes)
        {
            int totalScore = 0;

            foreach (var note in notes)
            {
                foreach (var rule in DiabetesRiskRules.Rules)
                {
                    if (note.Notes.Contains(rule.Pattern, StringComparison.OrdinalIgnoreCase))
                    {
                        totalScore += rule.ScoreImpact;
                    }
                }
            }

            int age = CalculateAge(patient.DateOfBirth);
            string risk = CalculateRisk(totalScore, age);

            return new DiabetesRisk
            {
                PatientId = patient.Id,
                RiskLevel = risk
            };
        }

        private int CalculateAge(DateTime birthDate)
        {
            int age = DateTime.Now.Year - birthDate.Year;
            if (birthDate > DateTime.Now.AddYears(-age)) age--;
            return age;
        }

        private string CalculateRisk(int score, int age)
        {
            // Ajustement simple : plus sévère si jeune
            if (age < 30)
                score += 1;
            // Pas de changement pour les 30 ans et plus

            if (score <= 0) return "None";
            if (score <= 2) return "Borderline";
            if (score <= 4) return "In Danger";
            return "Early Onset";
        }
    }
}