using System.Text.Json;

namespace ConcordiaCurriculumManager.Models;

public class Group : BaseModel
{
    public required string Name { get; set; }

    public List<User> Members { get; set; } = new();

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
