namespace ConcordiaCurriculumManager.Models.Curriculum;

public class CourseReference : BaseModel
{
    public required Guid CourseReferencingId { get; set; }

    public required Course CourseReferencing { get; set; }

    public required Guid CourseReferencedId { get; set; }

    public required Course CourseReferenced { get; set; }

}