using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;

namespace ConcordiaCurriculumManager.Models.Metrics;

public class DossierViewCount
{
    public required Guid DossierId { get; set; }
    public Dossier? Dossier { get; set; }
    public required int Count { get; set; }
}
