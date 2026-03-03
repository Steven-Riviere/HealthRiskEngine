namespace Shared.Models
{
    public static class DiabetesRiskRules
    {
        public static List<RiskRule> Rules { get; } = new()
    {
        new RiskRule { Pattern = "Hémoglobine A1C", ScoreImpact = 2 },
        new RiskRule { Pattern = "Microalbumine", ScoreImpact = 1 },
        new RiskRule { Pattern = "Cholestérol élevé", ScoreImpact = 2 },
        new RiskRule { Pattern = "Fumeur", ScoreImpact = 1 },
        new RiskRule { Pattern = "Vertiges", ScoreImpact = 1 },
        new RiskRule { Pattern = "amélioration glycémique", ScoreImpact = -2 },
        new RiskRule { Pattern = "glycémie stabilisée", ScoreImpact = -1 },
        new RiskRule { Pattern = "poids normal", ScoreImpact = -1 },
        new RiskRule { Pattern = "activité physique régulière", ScoreImpact = -2 },
        new RiskRule { Pattern = "alimentation équilibrée", ScoreImpact = -1 },
        new RiskRule { Pattern = "arrêt de la cigarette", ScoreImpact = -1 }
    };
    }
}
