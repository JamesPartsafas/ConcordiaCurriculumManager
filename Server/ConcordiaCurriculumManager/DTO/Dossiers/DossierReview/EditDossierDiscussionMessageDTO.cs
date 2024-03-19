
namespace ConcordiaCurriculumManager.DTO.Dossiers.DossierReview;

public class EditDossierDiscussionMessageDTO
{
    public required Guid DiscussionMessageId { get; set; }
    public required string NewMessage { get; set; }
}
