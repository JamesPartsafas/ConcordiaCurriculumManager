using ConcordiaCurriculumManager.Filters.Exceptions;

namespace ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;

public class DossierDiscussion : BaseModel
{
    public required Guid DossierId { get; set; }

    public Dossier? Dossier { get; set; }

    public IList<DiscussionMessage> Messages { get; set; } = new List<DiscussionMessage>();

    public void DeleteMessage(Guid messageId, Guid userId)
    {
        var message = Messages.Where(message => message.Id.Equals(messageId)).FirstOrDefault();

        if (message == null)
            throw new BadRequestException("There is no message with that ID to delete");

        message.MarkAsDeleted(userId);
    }
}