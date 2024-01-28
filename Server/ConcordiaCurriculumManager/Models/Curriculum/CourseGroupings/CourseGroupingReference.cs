using NpgsqlTypes;

namespace ConcordiaCurriculumManager.Models.Curriculum.CourseGroupings;

public class CourseGroupingReference : BaseModel
{
    public required Guid ParentGroupId { get; set; }
    public required Guid ChildGroupCommonIdentifier { get; set; }
    public required GroupingTypeEnum GroupingType { get; set; }
}

public enum GroupingTypeEnum
{
    [PgName(nameof(SubGrouping))]
    SubGrouping,

    [PgName(nameof(OptionalGrouping))]
    OptionalGrouping,
}
