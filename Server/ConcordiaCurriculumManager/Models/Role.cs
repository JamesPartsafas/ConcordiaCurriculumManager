using NpgsqlTypes;

namespace ConcordiaCurriculumManager.Models;

public enum RoleEnum
{
    [PgName(nameof(Initiator))]
    Initiator,
    
    [PgName(nameof(Admin))]
    Admin,

    [PgName(nameof(FacultyMemeber))]
    FacultyMemeber
}

public class Role: BaseModel
{
    public required RoleEnum UserRole { get; set; }

    public List<User> Users { get; set; } = new();
}
