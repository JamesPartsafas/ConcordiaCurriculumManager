using ConcordiaCurriculumManager.Models.Curriculum;

namespace ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests;

public class CourseCreationInitiationDTO : CourseRequestInitiationDTO
{
    public override CourseStateEnum GetAssociatedCourseState() => CourseStateEnum.NewCourseProposal;
}
