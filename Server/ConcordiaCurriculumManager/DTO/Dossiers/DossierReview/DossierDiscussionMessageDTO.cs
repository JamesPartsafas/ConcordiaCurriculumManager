namespace ConcordiaCurriculumManager.DTO.Dossiers.DossierReview;

public class DossierDiscussionMessageDTO
{
    public required Guid Id { get; set; }
    public required string Message { get; set; }
    public required Guid GroupId { get; set; }
    public Guid? ParentDiscussionMessageId { get; set; }
}
