namespace Shared.Models
{
    public static class CancersRiskRules
    {
        public static List<RiskRule> Rules { get; } = new()
    {
        new RiskRule { Pattern = "masse suspecte", ScoreImpact = 3 },
        new RiskRule { Pattern = "biopsie positive", ScoreImpact = 4 },
        new RiskRule { Pattern = "mutation génétique", ScoreImpact = 2 },
        new RiskRule { Pattern = "antécédent familial", ScoreImpact = 1 },
        new RiskRule { Pattern = "imagerie normale", ScoreImpact = -2 },
        new RiskRule { Pattern = "tumeur réséquée", ScoreImpact = -3 },
        new RiskRule { Pattern = "suivi régulier", ScoreImpact = -1 }
    };
    }
}
