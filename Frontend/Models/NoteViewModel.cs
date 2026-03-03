using System.ComponentModel.DataAnnotations;

namespace Frontend.Models
{
    public class NoteViewModel
    {
        public string? Id { get; set; }

        public int PatientId { get; set; }

        public DateTime DateAppointment { get; set; }

        [Required(ErrorMessage = "La note est obligatoire")]
        [Display(Name = "Notes du patient")]
        public string Notes { get; set; } = string.Empty;
    }

}
