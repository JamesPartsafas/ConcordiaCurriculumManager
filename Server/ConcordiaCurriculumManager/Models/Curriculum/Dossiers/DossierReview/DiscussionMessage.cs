using ConcordiaCurriculumManager.Models.Users;

namespace ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;

public class DiscussionMessage : BaseModel
{
    public required Guid DossierDiscussionId { get; set; }

    public DossierDiscussion? DossierDiscussion { get; set; }

    public required string Message { get; set; }

    public required Guid GroupId { get; set; }

    public Group? Group { get; set; }

    public required Guid AuthorId { get; set; }

    public User? Author { get; set; }

    public Guid? ParentDiscussionMessageId { get; set; }

    public DiscussionMessage? ParentDiscussionMessage { get; set; }
}