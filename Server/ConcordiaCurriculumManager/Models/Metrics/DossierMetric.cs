using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Users;

namespace ConcordiaCurriculumManager.Models.Metrics;

public class DossierMetric : BaseModel
{
    public required Guid DossierId { get; set; }
    public Dossier? Dossier { get; set; }
    public required Guid UserId { get; set; }
    public User? User { get; set; }
}
