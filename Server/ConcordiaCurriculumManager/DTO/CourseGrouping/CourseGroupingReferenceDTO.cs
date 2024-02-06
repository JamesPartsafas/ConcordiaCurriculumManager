using ConcordiaCurriculumManager.Models.Curriculum.CourseGroupings;

namespace ConcordiaCurriculumManager.DTO.CourseGrouping;

public class CourseGroupingReferenceDTO
{
    public required Guid Id { get; set; }
    public required Guid ParentGroupId { get; set; }
    public required Guid ChildGroupCommonIdentifier { get; set; }
    public required GroupingTypeEnum GroupingType { get; set; }
}
