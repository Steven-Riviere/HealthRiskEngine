using DiabetesAPI.Models;
using DiabetesAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiabetesAPI.Controllers
{
    [Route("api/diabetes")]
    [ApiController]
    public class DiabeteRiskController(IDiabetesRiskService diabetesRiskService) : ControllerBase
    {
        [HttpPost("assess")]
        public ActionResult<DiabetesRisk> Assess([FromBody] DiabetesRisk request)
        {
            var result = diabetesRiskService.AssessRisk(request.Patient, request.Notes);
            return Ok(result);
        }
    }
}
