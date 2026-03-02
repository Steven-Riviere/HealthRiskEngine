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
                    new() { PatientId = 1, DateAppointment = new DateTime(2025, 10, 18, 9, 30, 0), Notes = "Le patient déclare qu'il 'se sent très bien' Poids égal ou inférieur au poids recommandé" },
                    new() { PatientId = 2, DateAppointment = new DateTime(2025, 11, 05, 14, 0, 0), Notes = "Le patient déclare qu'il ressent beaucoup de stress au travail Il se plaint également que son audition est anormale dernièrement" },
                    new() { PatientId = 2, DateAppointment = new DateTime(2025, 11, 19, 10, 15, 0), Notes = "Le patient déclare avoir fait une réaction aux médicaments au cours des 3 derniers mois Il remarque également que son audition continue d'être anormale" },
                    new() { PatientId = 3, DateAppointment = new DateTime(2025, 12, 02, 11, 45, 0), Notes = "Le patient déclare qu'il fume depuis peu" },
                    new() { PatientId = 3, DateAppointment = new DateTime(2025, 12, 15, 15, 30, 0), Notes = "Le patient déclare qu'il est fumeur et qu'il a cessé de fumer l'année dernière Il se plaint également de crises d’apnée respiratoire anormales Tests de laboratoire indiquant un taux de cholestérol LDL élevé" },
                    new() { PatientId = 4, DateAppointment = new DateTime(2026, 01, 07, 8, 0, 0), Notes = "Le patient déclare qu'il lui est devenu difficile de monter les escaliers Il se plaint également d’être essoufflé Tests de laboratoire indiquant que les anticorps sont élevés Réaction aux médicaments" },
                    new() { PatientId = 4, DateAppointment = new DateTime(2026, 01, 18, 13, 15, 0), Notes = "Le patient déclare qu'il a mal au dos lorsqu'il reste assis pendant longtemps" },
                    new() { PatientId = 4, DateAppointment = new DateTime(2026, 02, 03, 16, 45, 0), Notes = "Le patient déclare avoir commencé à fumer depuis peu Hémoglobine A1C supérieure au niveau recommandé" },
                    new() { PatientId = 4, DateAppointment = new DateTime(2026, 02, 21, 9, 0, 0), Notes = "Taille, Poids, Cholestérol, Vertige et Réaction" }
                };
                notesCollection.InsertMany(notes);
            }
        }
    }
}
