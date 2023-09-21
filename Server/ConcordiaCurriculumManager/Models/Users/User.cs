using ConcordiaCurriculumManager.Models.Curriculum.Dossier;

namespace ConcordiaCurriculumManager.Models.Users;

public class User : BaseModel
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }

    public List<Role> Roles { get; set; } = new();

    public List<CourseCreationDossier> CourseCreationDossiers { get; set; } = new();
}
