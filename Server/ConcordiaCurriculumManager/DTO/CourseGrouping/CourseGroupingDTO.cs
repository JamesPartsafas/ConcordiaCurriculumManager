using ConcordiaCurriculumManager.DTO.Courses;
using ConcordiaCurriculumManager.Models.Curriculum.CourseGroupings;

namespace ConcordiaCurriculumManager.DTO.CourseGrouping;

public class CourseGroupingDTO : AbstractCourseGroupingDTO
{
    public Guid Id { get; set; }
    public required Guid CommonIdentifier { get; set; }
    public required CourseGroupingStateEnum State { get; set; }
    public int? Version { get; set; }
    public required bool Published { get; set; }
    public required ICollection<CourseGroupingReferenceDTO> SubGroupingReferences { get; set; } = new List<CourseGroupingReferenceDTO>();
    public required ICollection<CourseGroupingDTO> SubGroupings { get; set; } = new List<CourseGroupingDTO>();
    public required ICollection<CourseIdentifierDTO> CourseIdentifiers { get; set; } = new List<CourseIdentifierDTO>();
    public required ICollection<CourseDataDTO> Courses { get; set; } = new List<CourseDataDTO>();
    public required DateTime CreatedDate { get; set; }
    public required DateTime ModifiedDate { get; set; }
}
