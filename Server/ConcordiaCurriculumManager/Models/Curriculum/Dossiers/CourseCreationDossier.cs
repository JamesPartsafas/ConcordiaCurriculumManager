using ConcordiaCurriculumManager.Models.Users;

namespace ConcordiaCurriculumManager.Models.Curriculum.Dossier;

public class CourseCreationDossier : BaseModel
{
    public Guid InitiatorId { get; set; }

    public User Initiator { get; set; }

    public Guid NewCourseId { get; set; }

    public Course NewCourse { get; set; }
}
