using ConcordiaCurriculumManager.DTO.Dossiers;

namespace ConcordiaCurriculumManager.DTO.Metrics;

public class UserDossierViewedCountDTO
{
    public required Guid UserId { get; set; }
    public UserDTO? User { get; set; }
    public required int Count { get; set; }
}

public class UserDossierViewedCountWrapperDTO
{
    public required IList<UserDossierViewedCountDTO> Result { get; set; }
    public required int NextIndex { get; set; }
}