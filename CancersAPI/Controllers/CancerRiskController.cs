using CancersAPI.Models;
using CancersAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CancersAPI.Controllers
{
    [Route("api/cancers")]
    [ApiController]
    public class CancerRiskController(CancersRiskService cancersRiskService) : ControllerBase
    {
        [HttpPost("assess")]
        public ActionResult<CancersRisk> Assess([FromBody] CancersRisk request)
        {
            var result = cancersRiskService.AssessRisk(request.Patient, request.Notes);
            return Ok(result);
        }
    }
}
