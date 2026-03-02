using DiabetesAPI.Models;
using Shared.Models;

namespace DiabetesAPI.Services
{
    public class DiabetesRiskService : IDiabetesRiskService
    {
        private static readonly List<RiskRule> RiskRules = new()
        {
            // phrases qui augmentent le risque
            new() { Pattern = "Hémoglobine A1C", ScoreImpact = 2 },
            new() { Pattern = "Microalbumine", ScoreImpact = 1 },
            new() { Pattern = "Cholestérol élevé", ScoreImpact = 2 },
            new() { Pattern = "Fumeur", ScoreImpact = 1 },
            new() { Pattern = "Vertiges", ScoreImpact = 1 },

            // phrases qui diminuent le risque
            new() { Pattern = "amélioration glycémique", ScoreImpact = -2 },
            new() { Pattern = "glycémie stabilisée", ScoreImpact = -1 },
            new() { Pattern = "poids normal", ScoreImpact = -1 },
            new() { Pattern = "activité physique régulière", ScoreImpact = -2 },
            new() { Pattern = "alimentation équilibrée", ScoreImpact = -1 },
            new() { Pattern = "arrêt de la cigarette", ScoreImpact = -1 }

        };

        public DiabetesRisk AssessRisk(PatientDto patient, List<NoteDto> notes)
        {
            // Calcul du score total
            int totalScore = notes.Sum(note =>
                RiskRules
                    .Where(rule => note.Notes.Contains(rule.Pattern, StringComparison.OrdinalIgnoreCase))
                    .Sum(rule => rule.ScoreImpact)
            );

            string riskLevel = CalculateRisk(patient, totalScore);

            return new DiabetesRisk
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

            //Ajustement selon le genre (exemple cohérent)
            if (age < 30)
            {
                if (patient.Gender == GenderEnum.Male)
                    score += 1;
                else
                    score += 2;
            }

            if (score <= 0) return "None";
            if (score <= 3) return "Borderline";
            if (score <= 6) return "In Danger";
            return "Early Onset";
        }


    }
}
