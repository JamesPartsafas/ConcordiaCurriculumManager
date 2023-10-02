using ConcordiaCurriculumManager.Models.Users;

namespace ConcordiaCurriculumManager.Models.Curriculum.Dossiers;

public class CourseCreationRequest : BaseModel
{
    public Guid InitiatorId { get; set; }

    public User Initiator { get; set; }

    public Guid NewCourseId { get; set; }

    public Course NewCourse { get; set; }

    public Guid DossierId {  get; set; }

    public Dossier Dossier { get; set; }
}
