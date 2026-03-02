using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NotesAPI.Models
{
    public class Note
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public int PatientId { get; set; }
        public DateTime DateAppointment { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
