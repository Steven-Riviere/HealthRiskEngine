
namespace Shared.Models
{
    public class RiskRule
    {
        public string Pattern { get; set; } = ""; // phrase ou mot déclencheur
        public int ScoreImpact { get; set; } // positif ou négatif
    }
}