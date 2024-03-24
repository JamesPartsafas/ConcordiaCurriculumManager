namespace ConcordiaCurriculumManager.DTO.Dossiers.DossierReview;

public class DossierDiscussionMessageDTO
{
    public required Guid Id { get; set; }
    public required UserDTO Author { get; set; }
    public required string Message { get; set; }
    public required Guid GroupId { get; set; }
    public Guid? ParentDiscussionMessageId { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime ModifiedDate { get; set; }
    public int VoteCount { get; set; } = 0;
    public IEnumerable<DiscussionMessageVoteDTO> DiscussionMessageVotes { get; set; } = new List<DiscussionMessageVoteDTO>();
    public required bool IsDeleted { get; set; }
}
