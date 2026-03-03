using MongoDB.Driver;
using NotesAPI.Models;

namespace NotesAPI.Data
{
    public static class NoteData
    {
        public static void SeedNotes(IMongoDatabase database)
        {
            var notesCollection = database.GetCollection<Note>("Notes");
            // Check if the collection is empty before seeding
            if (notesCollection.CountDocuments(FilterDefinition<Note>.Empty) == 0)
            {
                var notes = new List<Note>
                {
                    new() { PatientId = 1, DateAppointment = new DateTime(2025, 10, 18, 9, 30, 0), Notes = "Le patient déclare qu'il 'se sent très bien'. Poids normal. Activité physique régulière. Arrêt de la cigarette." },
                    new() { PatientId = 2, DateAppointment = new DateTime(2025, 11, 05, 14, 0, 0), Notes = "Le patient déclare qu'il ressent beaucoup de stress au travail Il se plaint également que son audition est anormale dernièrement" },
                    new() { PatientId = 2, DateAppointment = new DateTime(2025, 11, 19, 10, 15, 0), Notes = "Fumeur depuis 5 ans. Hémoglobine A1C élevée. Cholestérol élevé. Antécédent familial de cancer." },
                    new() { PatientId = 3, DateAppointment = new DateTime(2025, 12, 02, 11, 45, 0), Notes = "Le patient déclare qu'il fume depuis peu" },
                    new() { PatientId = 3, DateAppointment = new DateTime(2025, 12, 15, 15, 30, 0), Notes = "Amélioration glycémique. Glycémie stabilisée. Imagerie normale. Tumeur réséquée." },
                    new() { PatientId = 4, DateAppointment = new DateTime(2026, 01, 07, 8, 0, 0), Notes = "Le patient déclare qu'il lui est devenu difficile de monter les escaliers Il se plaint également d’être essoufflé Tests de laboratoire indiquant que les anticorps sont élevés Réaction aux médicaments" },
                    new() { PatientId = 4, DateAppointment = new DateTime(2026, 01, 18, 13, 15, 0), Notes = "Le patient déclare qu'il a mal au dos lorsqu'il reste assis pendant longtemps" },
                    new() { PatientId = 4, DateAppointment = new DateTime(2026, 02, 03, 16, 45, 0), Notes = "Hémoglobine A1C supérieure au niveau recommandé. Vertiges. Masse suspecte. Biopsie positive." },
                    new() { PatientId = 4, DateAppointment = new DateTime(2026, 02, 21, 9, 0, 0), Notes = "Taille, Poids, Cholestérol, Vertige et Réaction" }
                };
                notesCollection.InsertMany(notes);
            }
        }
    }
}
