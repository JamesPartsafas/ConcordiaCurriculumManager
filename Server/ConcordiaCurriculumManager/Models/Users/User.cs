using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Models.Metrics;

namespace ConcordiaCurriculumManager.Models.Users;

public class User : BaseModel
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }

    public List<Role> Roles { get; set; } = new();

    public List<Group> Groups { get; set; } = new();

    public List<Group> MasteredGroups { get; set; } = new();

    public List<Dossier> Dossiers { get; set; } = new();

    public List<DossierMetric> DossierMetrics { get; set; } = new();

    public List<DiscussionMessageVote> DiscussionMessageVotes { get; set; } = new();

    public Guid? ResetPasswordToken { get; set; }
}