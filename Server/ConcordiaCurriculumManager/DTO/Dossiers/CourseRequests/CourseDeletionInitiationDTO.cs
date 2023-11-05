using ConcordiaCurriculumManager.Models.Curriculum;

namespace ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests;

public class CourseDeletionInitiationDTO : CourseInitiationDTO
{
    public required string Subject { get; set; }

    public required string Catalog { get; set; }
}
