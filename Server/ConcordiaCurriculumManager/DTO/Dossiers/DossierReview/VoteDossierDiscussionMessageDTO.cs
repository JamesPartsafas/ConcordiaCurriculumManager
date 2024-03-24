namespace ConcordiaCurriculumManager.DTO.Dossiers.DossierReview;

public class VoteDossierDiscussionMessageDTO
{
    public required Guid DiscussionMessageId { get; set; }
    public required VoteDossierDiscussionMessageValue Value { get; set; }
}

public enum VoteDossierDiscussionMessageValue
{
    Upvote = 0,
    Downvote = 1,
    NoVote = 2
}
