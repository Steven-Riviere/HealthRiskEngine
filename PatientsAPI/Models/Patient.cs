using Shared.Models;

namespace PatientsAPI.Models;

public class Patient
{
    public int Id { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public GenderEnum Gender { get; set; }
    public string? City { get; set; }
    public string? Phone { get; set; }
}
