namespace ConcordiaCurriculumManager.DTO.CourseGrouping;

public class CourseGroupingModificationInputDTO : CourseGroupingInputDTO
{
    public required Guid CommonIdentifier { get; set; }
}
