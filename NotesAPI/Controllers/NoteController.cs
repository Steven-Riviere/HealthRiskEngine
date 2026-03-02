using Microsoft.AspNetCore.Mvc;
using NotesAPI.Models;
using NotesAPI.Services;

namespace NotesAPI.Controllers
{
    [Route("api/notes")]
    [ApiController]
    public class NoteController(INoteService noteService) : ControllerBase
    {

        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<List<Note>>> GetNotesByPatient(int patientId)
        {
            var note = await noteService.GetNotesByPatientIdAsync(patientId);
            if (note == null || note.Count == 0)
                return NotFound($"Aucune note trouvée pour le patient {patientId}");
            return Ok(note);
        }

        // POST: api/notes
        [HttpPost]
        public async Task<ActionResult<Note>> CreateNote([FromBody] Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await noteService.CreateNoteAsync(note);
            return CreatedAtAction(nameof(GetNotesByPatient), new { patientId = note.PatientId }, note);
        }
    }
}
