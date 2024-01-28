using ConcordiaCurriculumManager.Models.Curriculum.CourseGroupings;

namespace ConcordiaCurriculumManager.Models.Curriculum.Dossiers;

public class CourseGroupingRequest : CourseRequest
{
    public required Guid CourseGroupingId { get; set; }

    public CourseGrouping? CourseGrouping { get; set; }
}
