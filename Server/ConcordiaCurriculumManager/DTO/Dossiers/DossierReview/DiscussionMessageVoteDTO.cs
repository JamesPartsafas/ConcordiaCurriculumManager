namespace ConcordiaCurriculumManager.DTO.Dossiers.DossierReview;

public class DiscussionMessageVoteDTO
{
    public required Guid DiscussionMessageId { get; set; }

    public required Guid UserId { get; set; }

    public required VoteDossierDiscussionMessageValue DiscussionMessageVoteValue { get; set; }
}
