using ConcordiaCurriculumManager.Models.Users;

namespace ConcordiaCurriculumManager.Models.Metrics;

public class UserDossierViewedCount
{
    public required Guid UserId { get; set; }
    public User? User { get; set; }
    public required int Count { get; set; }
}
