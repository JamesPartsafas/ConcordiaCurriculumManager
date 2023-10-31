using ConcordiaCurriculumManager.Models.Curriculum;

namespace ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests;

public class CourseDeletionInitiationDTO : CourseInitiationDTO
{
    public required int CourseId { get; set; }
}
