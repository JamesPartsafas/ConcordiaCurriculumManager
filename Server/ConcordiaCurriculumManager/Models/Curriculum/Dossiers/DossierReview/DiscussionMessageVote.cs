using ConcordiaCurriculumManager.Models.Users;

namespace ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;

public class DiscussionMessageVote : BaseModel
{
    public required Guid DiscussionMessageId { get; set; }

    public DiscussionMessage? DiscussionMessage { get; set; }

    public required Guid UserId { get; set; }

    public User? User { get; set; }
    
    public required DiscussionMessageVoteValue DiscussionMessageVoteValue { get; set; }
}

public enum DiscussionMessageVoteValue
{
    Upvote = 0,
    Downvote = 1
}
