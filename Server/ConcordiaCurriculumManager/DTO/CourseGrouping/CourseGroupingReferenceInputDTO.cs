using ConcordiaCurriculumManager.Models.Curriculum.CourseGroupings;

namespace ConcordiaCurriculumManager.DTO.CourseGrouping;

public class CourseGroupingReferenceInputDTO
{
    public required Guid ChildGroupCommonIdentifier { get; set; }
    public required GroupingTypeEnum GroupingType { get; set; }
}
