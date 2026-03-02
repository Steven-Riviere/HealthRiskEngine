namespace CancersAPI.Models
{
    public class CancersRisk
    {
        public int PatientId { get; set; }
        public string RiskLevel { get; set; } = string.Empty; 
        public Shared.Models.PatientDto Patient { get; set; } = new();
        public List<Shared.Models.NoteDto> Notes { get; set; } = new();
    }
}
