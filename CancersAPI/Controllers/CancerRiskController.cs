using CancersAPI.Models;
using CancersAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CancersAPI.Controllers
{
    [Route("api/cancers")]
    [ApiController]
    public class CancerRiskController(ICancersRiskService _cancersRiskService) : ControllerBase
    {
        [HttpPost("assess")]
        public ActionResult<CancersRisk> Assess([FromBody] CancersRisk request)
        {
            var result = _cancersRiskService.AssessRisk(request.Patient, request.Notes);
            return Ok(result);
        }
    }
}
