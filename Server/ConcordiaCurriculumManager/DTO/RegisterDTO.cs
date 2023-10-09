using System.ComponentModel.DataAnnotations;

namespace ConcordiaCurriculumManager.DTO;

public class RegisterDTO
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    [EmailAddress]
    public required string Email { get; set; }

    public required string Password { get; set; }
}
