using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;

namespace ConcordiaCurriculumManager.DTO.Dossiers;

public class DossierDTO
{
    public required Guid Id { get; set; }

    public required Guid InitiatorId { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public required DossierStateEnum State { get; set; }
}
