using PatientsAPI.Data;
using Microsoft.EntityFrameworkCore;
using PatientsAPI.Models;

namespace PatientsAPI.Services
{
    public class PatientService : IPatientService
    {
        private readonly PatientsDbContext _context;

        public PatientService(PatientsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
            => await _context.Patients.ToListAsync();

        public async Task<Patient?> GetPatientByIdAsync(int id)
            => await _context.Patients.FindAsync(id);

        public async Task AddPatientAsync(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            var existingPatient = await _context.Patients.FindAsync(patient.Id);
            if (existingPatient == null)
            {
                throw new KeyNotFoundException($"Patient with Id {patient.Id} not found.");
            }
            // Mise à jour des propriétés une par une
            existingPatient.FirstName = patient.FirstName;
            existingPatient.LastName = patient.LastName;
            existingPatient.DateOfBirth = patient.DateOfBirth;
            existingPatient.Gender = patient.Gender;
            existingPatient.City = patient.City;
            existingPatient.Phone = patient.Phone;

            await _context.SaveChangesAsync();
        }

        public async Task DeletePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }
    }
}
