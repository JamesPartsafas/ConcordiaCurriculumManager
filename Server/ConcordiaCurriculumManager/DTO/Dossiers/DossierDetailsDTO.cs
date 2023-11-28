using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;

namespace ConcordiaCurriculumManager.DTO.Dossiers;

public class DossierDetailsDTO
{
    public required Guid Id { get; set; }

    public required Guid InitiatorId { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public required bool Published { get; set; }

    public List<CourseCreationRequest> CourseCreationRequests { get; set; } = new List<CourseCreationRequest>();

    public List<CourseModificationRequest> CourseModificationRequests { get; set; } = new List<CourseModificationRequest>();

    public List<CourseDeletionRequest> CourseDeletionRequests { get; set; } = new List<CourseDeletionRequest>();

    public List<ApprovalStage> ApprovalStages { get; set; } = new List<ApprovalStage>();

    public required DateTime CreatedDate { get; set; }

    public required DateTime ModifiedDate { get; set; }
}
