using System.ComponentModel.DataAnnotations;

namespace ConcordiaCurriculumManager.DTO;

public class UserDTO
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    [EmailAddress]
    public required string Email { get; set; }
}
