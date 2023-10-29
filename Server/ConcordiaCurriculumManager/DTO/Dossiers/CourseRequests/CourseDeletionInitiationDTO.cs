using ConcordiaCurriculumManager.Models.Curriculum;

namespace ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests;

public class CourseDeletionInitiationDTO : CourseRequestInitiationDTO
{
    public required int CourseId { get; set; }

    public override CourseStateEnum GetAssociatedCourseState() => CourseStateEnum.CourseDeletionProposal;
}
