namespace ConcordiaCurriculumManager.Models.Curriculum.Dossiers;

public class CourseCreationRequest : CourseRequest
{
    public required Guid NewCourseId { get; set; }

    public Course? NewCourse { get; set; }
}
