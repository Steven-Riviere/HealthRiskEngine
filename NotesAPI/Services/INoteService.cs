using NotesAPI.Models;

namespace NotesAPI.Services
{
    public interface INoteService
    {
        Task<Note> CreateNoteAsync(Note note);
        Task<List<Note>> GetNotesByPatientIdAsync(int patientId);
    }
}
