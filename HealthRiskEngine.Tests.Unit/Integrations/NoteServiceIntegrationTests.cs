using MongoDB.Driver;
using NotesAPI.Models;
using NotesAPI.Services;

namespace HealthRiskEngine.Tests.Integrations
{
    public class NoteServiceIntegrationTests : IAsyncLifetime
    {
        private IMongoDatabase _database;
        private NoteService _service;
        private IMongoCollection<Note> _collection;

        public async Task InitializeAsync()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            _database = client.GetDatabase("Notes_TestIntegration");
            _collection = _database.GetCollection<Note>("Notes");

            //On Cleane avant chaque test
            await _collection.DeleteManyAsync(_ => true);

            _service = new NoteService(_database);
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        [Fact]
        public async Task CreateNoteAsync_ShouldCreateNote()
        {
            // Arrange
            var note = new Note
            {
                PatientId = 1,
                Notes = "Test note content"
            };
            // Act
            var result = await _service.CreateNoteAsync(note);

            // Assert
            var notesInDb = await _collection.Find(n => n.PatientId == 1).ToListAsync();

            Assert.Single(notesInDb);
            Assert.NotEqual(default(DateTime), result.DateAppointment);
        }

        [Fact]
        public async Task GetNotesByPatientIdAsync_ShouldReturnNotes()
        {
            // Arrange
            var now = DateTime.Now;
            var note1 = new Note
            {
                PatientId = 2,
                DateAppointment = now.AddDays(-1),
                Notes = "Note 1"
            };

            var note2 = new Note 
            { 
                PatientId = 2, 
                DateAppointment = now, 
                Notes = "Note 2" 
            };

            await _collection.InsertManyAsync(new[] { note1, note2 });

            // Act
            var result = await _service.GetNotesByPatientIdAsync(2);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Note 2", result.First().Notes); 
            Assert.True(result.First().DateAppointment >= result.Last().DateAppointment);
        }

    }
}
