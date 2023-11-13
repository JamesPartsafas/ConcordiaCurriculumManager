using ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;
using System.Text.Json;

namespace ConcordiaCurriculumManager.Models.Users;

public class Group : BaseModel
{
    public required string Name { get; set; }

    public List<User> Members { get; set; } = new();

    public List<User> GroupMasters { get; set; } = new();

    public ICollection<ApprovalStage>? ApprovalStages { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}
