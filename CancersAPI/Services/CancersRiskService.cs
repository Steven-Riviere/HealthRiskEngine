using CancersAPI.Models;
using Shared.Models;

namespace CancersAPI.Services
{
    public class CancersRiskService : ICancersRiskService
    {
        public CancersRisk AssessRisk(PatientDto patient, List<NoteDto> notes)
        {
            // Calcul du score total
            int totalScore = 0;

            foreach (var note in notes)
            {
                foreach (var rule in CancersRiskRules.Rules)
                {
                    if (note.Notes.Contains(rule.Pattern, StringComparison.OrdinalIgnoreCase))
                    {
                        totalScore += rule.ScoreImpact;
                    }
                }
            }

            int age = CalculateAge(patient.DateOfBirth);
            string risk = CalculateRisk(totalScore, age);

            return new CancersRisk
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
            if (age > 60)
                score += 2;
            else if (age > 45)
                score += 1;

            if (score <= 0) return "None";
            if (score <= 2) return "Borderline";
            if (score <= 4) return "In Danger";
            return "Early Onset";
        }
    }
}