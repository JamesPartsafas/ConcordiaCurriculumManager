using System.ComponentModel.DataAnnotations;

namespace ConcordiaCurriculumManager.DTO;

public class LoginDTO
{
    public const string MediaType = "application/x.ccm.authentication+json;v=1";

    [EmailAddress]
    public required string Email { get; set; }

    public required string Password { get; set; }
}
