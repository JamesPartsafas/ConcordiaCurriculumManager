using ConcordiaCurriculumManager.Models.Curriculum;

namespace ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.InputDTOs;

public class CourseCreationInitiationDTO : CourseRequestInitiationDTO
{
    public override CourseStateEnum GetAssociatedCourseState() => CourseStateEnum.NewCourseProposal;
}
