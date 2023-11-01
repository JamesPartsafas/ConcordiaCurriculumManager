namespace ConcordiaCurriculumManager.Models.Curriculum.Dossiers;

public class CourseDeletionRequest : CourseRequest
{
    public required Guid CourseId { get; set; }

    public Course? Course { get; set; }

}
