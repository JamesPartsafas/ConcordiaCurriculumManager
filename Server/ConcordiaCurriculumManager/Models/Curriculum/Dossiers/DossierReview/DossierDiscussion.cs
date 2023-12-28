namespace ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;

public class DossierDiscussion : BaseModel
{
    public required Guid DossierId { get; set; }

    public Dossier? Dossier { get; set; }

    public IList<DiscussionMessage> Messages { get; set; } = new List<DiscussionMessage>();
}