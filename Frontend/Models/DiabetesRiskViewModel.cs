using Frontend.Models;

namespace Frontend.Models
{
    public class DiabetesRiskViewModel
    {
        public string RiskLevel { get; set; } = string.Empty;
        public string RiskColor { get; set; } = string.Empty;

        public PatientViewModel Patient { get; set; } = new PatientViewModel();
        public List<NoteViewModel> Notes { get; set; } = new();

        // Ajout : logique de présentation
        public string DisplayName => RiskLevel switch
        {
            "None" => "Aucun risque de diabète",
            "Borderline" => "Diabète à surveiller",
            "In Danger" => "Attention suspicion de diabète",
            "Early Onset" => "Risque de diabète élevé",
            _ => RiskLevel
        };

        public string BgClass => RiskLevel switch
        {
            "None" => "bg-success text-white",
            "Borderline" => "bg-warning text-dark",
            "In Danger" => "bg-orange text-dark",
            "Early Onset" => "bg-danger text-white",
            _ => "bg-secondary text-white"
        };

        public string IconClass => RiskLevel switch
        {
            "None" => "fa-solid fa-circle-check",
            "Borderline" => "fa-solid fa-triangle-exclamation",
            "In Danger" => "fa-solid fa-triangle-exclamation",
            "Early Onset" => "fa-solid fa-skull-crossbones",
            _ => "fa-solid fa-question-circle"
        };

    }
}
