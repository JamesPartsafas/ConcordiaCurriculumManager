using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.OutputDTOs;
using ConcordiaCurriculumManager.DTO.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;

namespace ConcordiaCurriculumManager.DTO.Dossiers;

public class DossierDetailsDTO
{
    public required Guid Id { get; set; }

    public required Guid InitiatorId { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public required DossierStateEnum State { get; set; }

    public List<CourseCreationRequestCourseDetailsDTO> CourseCreationRequests { get; set; } = new List<CourseCreationRequestCourseDetailsDTO>();

    public List<CourseModificationRequestCourseDetailsDTO> CourseModificationRequests { get; set; } = new List<CourseModificationRequestCourseDetailsDTO>();

    public List<CourseDeletionRequestCourseDetailsDTO> CourseDeletionRequests { get; set; } = new List<CourseDeletionRequestCourseDetailsDTO>();

    public List<ApprovalStageDTO> ApprovalStages { get; set; } = new List<ApprovalStageDTO>();

    public required DossierDiscussionDTO Discussion { get; set; }

    public required DateTime CreatedDate { get; set; }

    public required DateTime ModifiedDate { get; set; }
}
