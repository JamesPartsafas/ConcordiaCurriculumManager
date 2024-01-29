using ConcordiaCurriculumManager.DTO.Courses;
using ConcordiaCurriculumManager.Models.Curriculum.CourseGroupings;

namespace ConcordiaCurriculumManager.DTO.CourseGrouping;

public abstract class AbstractCourseGroupingDTO
{
    public required string Name { get; set; }
    public required string RequiredCredits { get; set; }
    public required bool IsTopLevel { get; set; }
    public required SchoolEnum School { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
}
