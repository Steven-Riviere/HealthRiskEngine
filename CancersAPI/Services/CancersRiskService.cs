using CancersAPI.Models;
using Shared.Models;

namespace CancersAPI.Services
{
    public class CancersRiskService : ICancersRiskService
    {
        private static readonly List<Shared.Models.RiskRule> RiskRules = new()
        {
            // phrases qui augmentent le risque cancer
            new() { Pattern = "masse suspecte", ScoreImpact = 3 },
            new() { Pattern = "biopsie positive", ScoreImpact = 4 },
            new() { Pattern = "mutation génétique", ScoreImpact = 2 },
            new() { Pattern = "antécédent familial", ScoreImpact = 1 },

            // phrases qui diminuent le risque (prévention / amélioration)
            new() { Pattern = "imagerie normale", ScoreImpact = -2 },
            new() { Pattern = "tumeur réséquée", ScoreImpact = -3 },
            new() { Pattern = "suivi régulier", ScoreImpact = -1 }
        };

        public CancersRisk AssessRisk(PatientDto patient, List<NoteDto> notes)
        {
            // Calcul du score total
            int totalScore = notes.Sum(note =>
                RiskRules
                    .Where(rule => note.Notes.Contains(rule.Pattern, StringComparison.OrdinalIgnoreCase))
                    .Sum(rule => rule.ScoreImpact)
            );

            string riskLevel = CalculateRisk(patient, totalScore);

            return new CancersRisk
            {
                PatientId = patient.Id,
                Patient = patient,
                Notes = notes,
                RiskLevel = riskLevel
            };

        }
        private string CalculateRisk(PatientDto patient, int score)
        {
            int age = DateTime.Now.Year - patient.DateOfBirth.Year;
            if (patient.DateOfBirth > DateTime.Now.AddYears(-age))
                age--;

            // Ajustement selon l'âge
            if (age >= 60)
                score += 2;
            else if (age >= 40)
                score += 1;

            // Ajustement selon le sexe
            if (age<30 )
            {
                if (patient.Gender == GenderEnum.Male)
                    score += 1;
                else
                    score += 2;
            }

            if (score <= 0) return "Rien";
            if (score <= 4) return "Risque";
            if (score <= 7) return "En Danger";
            return "Risque élevé";
        }
    }
}
