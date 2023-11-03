using ConcordiaCurriculumManager.Models.Curriculum;

namespace ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests;

public abstract class CourseRequestInitiationDTO : CourseInitiationBaseDataDTO
{
    public required string Subject { get; set; }

    public required string Catalog { get; set; }

    public abstract CourseStateEnum GetAssociatedCourseState();
}
