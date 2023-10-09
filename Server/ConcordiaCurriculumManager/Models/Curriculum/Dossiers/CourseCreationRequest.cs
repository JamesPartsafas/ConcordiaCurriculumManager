using ConcordiaCurriculumManager.Models.Users;

namespace ConcordiaCurriculumManager.Models.Curriculum.Dossiers;

public class CourseCreationRequest : BaseModel
{
    public required Guid NewCourseId { get; set; }

    public Course? NewCourse { get; set; }

    public required Guid DossierId {  get; set; }

    public Dossier? Dossier { get; set; }
}
