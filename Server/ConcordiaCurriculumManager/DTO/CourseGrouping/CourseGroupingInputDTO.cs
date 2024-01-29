using ConcordiaCurriculumManager.DTO.Courses;
using ConcordiaCurriculumManager.Models.Curriculum.CourseGroupings;

namespace ConcordiaCurriculumManager.DTO.CourseGrouping;

public class CourseGroupingInputDTO : AbstractCourseGroupingDTO
{
    public required ICollection<CourseGroupingReferenceInputDTO> SubGroupingReferences { get; set; } = new List<CourseGroupingReferenceInputDTO>();
    public required ICollection<CourseIdentifierDTO> CourseIdentifiers { get; set; } = new List<CourseIdentifierDTO>();
}
