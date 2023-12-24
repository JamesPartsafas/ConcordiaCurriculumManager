using ConcordiaCurriculumManager.DTO.Dossiers.DossierReview;

namespace ConcordiaCurriculumManager.DTO.Dossiers;

public class DossierDetailsWithDiscussionDTO : DossierDetailsDTO
{
    public required DossierDiscussionDTO Discussion { get; set; }
}
