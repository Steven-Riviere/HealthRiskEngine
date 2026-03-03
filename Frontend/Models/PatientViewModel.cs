using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Shared.Models;

namespace Frontend.Models
{
    public class PatientViewModel
    {
        public int Id { get; set; }

        [DisplayName("Nom")]
        [Required(ErrorMessage = "Le nom est obligatoire.")]
        public string LastName { get; set; } = string.Empty;

        [DisplayName("Prénom")]
        [Required(ErrorMessage = "Le prénom est obligatoire.")]
        public string FirstName { get; set; } = string.Empty;

        [DisplayName("Date de naissance")]
        [Required(ErrorMessage = "La date de naissance est obligatoire.")]
        public DateTime DateOfBirth { get; set; }

        [DisplayName("Âge")]
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - DateOfBirth.Year;
                if (DateOfBirth.Date > today.AddYears(-age)) age--;
                return age;
            }
        }

        [DisplayName("Genre")]
        [Required(ErrorMessage = "Le genre est obligatoire.")]
        public GenderEnum Gender { get; set; }

        [DisplayName("Ville")]
        public string? City { get; set; }

        [DisplayName("N° de tél")]
        public string? Phone { get; set; }

        public List<NoteViewModel> Notes { get; set; } = new();

        public DiabetesRiskViewModel? DiabetesRisk { get; set; }
        public CancersRiskViewModel? CancersRisk { get; set; }

    }
}
