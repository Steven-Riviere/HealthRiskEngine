using Microsoft.AspNetCore.Mvc;
using PatientsAPI.Models;
using PatientsAPI.Services;

namespace PatientsAPI.Controllers
{
    [Route("api/patients")]
    [ApiController]
    public class PatientController(IPatientService patientService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<Patient>> GetAllPatients()
        {
            var patients = await patientService.GetAllPatientsAsync();

            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatientById(int id)
        {
            var patient = await patientService.GetPatientByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        [HttpPost]
        public async Task<ActionResult<Patient>> AddPatient([FromBody] Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await patientService.AddPatientAsync(patient);
            return CreatedAtAction(nameof(GetPatientById), new { id = patient.Id }, patient);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Patient>> UpdatePatient(int id, [FromBody] Patient patient)
        {
            if (id != patient.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingPatient = await patientService.GetPatientByIdAsync(id);
            if (existingPatient == null)
            {
                return NotFound();
            }

            await patientService.UpdatePatientAsync(patient);
            return NoContent();
        }

    }
}