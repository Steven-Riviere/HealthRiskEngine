using MongoDB.Driver;
using NotesAPI.Models;

namespace NotesAPI.Services
{
    public class NoteService : INoteService
    {
        private readonly IMongoCollection<Note> _notes;

        public NoteService(IMongoDatabase database)
        {
            _notes = database.GetCollection<Note>("Notes");
        }

        public async Task<Note> CreateNoteAsync(Note note)
        {
            if (note.DateAppointment == default)
                note.DateAppointment = DateTime.Now;

            await _notes.InsertOneAsync(note);
            return note;
        }

        public async Task<List<Note>> GetNotesByPatientIdAsync(int patientId)
        {
            return await _notes
                .Find(note => note.PatientId == patientId)
                .SortByDescending(note => note.DateAppointment)
                .ToListAsync();
        }


    }
}
