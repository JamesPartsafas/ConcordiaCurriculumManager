using NpgsqlTypes;

namespace ConcordiaCurriculumManager.Models.Users;

public enum RoleEnum
{
    [PgName(nameof(Initiator))]
    Initiator,

    [PgName(nameof(Admin))]
    Admin,

    [PgName(nameof(FacultyMember))]
    FacultyMember
}

public class Role : BaseModel
{
    public required RoleEnum UserRole { get; set; }

    public List<User> Users { get; set; } = new();
}
