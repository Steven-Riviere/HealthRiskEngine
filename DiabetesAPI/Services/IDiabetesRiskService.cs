using DiabetesAPI.Models;
using Shared.Models;

namespace DiabetesAPI.Services
{
    public interface IDiabetesRiskService
    {
        DiabetesRisk AssessRisk(PatientDto patient, List<NoteDto> notes);
    }
}
