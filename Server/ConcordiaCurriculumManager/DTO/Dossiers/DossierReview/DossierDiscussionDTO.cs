namespace ConcordiaCurriculumManager.DTO.Dossiers.DossierReview;

public class DossierDiscussionDTO
{
    public required Guid DossierId { get; set; }
    public required IList<DossierDiscussionMessageDTO> Messages { get; set; }
}
