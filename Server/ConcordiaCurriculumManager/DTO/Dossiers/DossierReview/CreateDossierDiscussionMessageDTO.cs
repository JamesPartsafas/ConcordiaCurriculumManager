namespace ConcordiaCurriculumManager.DTO.Dossiers.DossierReview;

public class CreateDossierDiscussionMessageDTO
{
    public required string Message { get; set; }
    public required Guid GroupId { get; set; }
    public Guid? ParentDiscussionMessageId { get; set; }
}
