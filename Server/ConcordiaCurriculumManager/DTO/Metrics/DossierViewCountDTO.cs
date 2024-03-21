using ConcordiaCurriculumManager.DTO.Dossiers;

namespace ConcordiaCurriculumManager.DTO.Metrics;

public class DossierViewCountDTO
{
    public required Guid DossierId { get; set; }
    public DossierDTO? Dossier { get; set; }
    public required int Count { get; set; }
}

public class DossierViewCountWrapperDTO
{
    public required IList<DossierViewCountDTO> Result { get; set; }
    public required int NextIndex { get; set; }
}
