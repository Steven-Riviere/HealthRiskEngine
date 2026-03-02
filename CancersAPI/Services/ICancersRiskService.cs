using CancersAPI.Models;
using Shared.Models;

namespace CancersAPI.Services
{
    public interface ICancersRiskService
    {
        CancersRisk AssessRisk(PatientDto patient, List<NoteDto> notes);
    }
}
