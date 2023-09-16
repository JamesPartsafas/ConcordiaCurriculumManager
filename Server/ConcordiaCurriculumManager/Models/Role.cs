using NpgsqlTypes;
using System.Text.Json;

namespace ConcordiaCurriculumManager.Models;

public enum RoleEnum
{
    [PgName(nameof(Initiator))]
    Initiator,
    
    [PgName(nameof(Admin))]
    Admin,

    [PgName(nameof(FacultyMember))]
    FacultyMember
}

public class Role: BaseModel
{
    public required RoleEnum UserRole { get; set; }

    public List<User> Users { get; set; } = new();

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
