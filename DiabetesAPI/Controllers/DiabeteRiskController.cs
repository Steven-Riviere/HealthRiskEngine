using DiabetesAPI.Models;
using DiabetesAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiabetesAPI.Controllers
{
    [Route("api/diabetes")]
    [ApiController]
    public class DiabetesRiskController(IDiabetesRiskService _diabetesRiskService) : ControllerBase
    {

        [HttpPost("assess")]
        public ActionResult<DiabetesRisk> Assess([FromBody] DiabetesRisk request)
        {
            var result = _diabetesRiskService.AssessRisk(request.Patient, request.Notes);
            return Ok(result);
        }
    }
}