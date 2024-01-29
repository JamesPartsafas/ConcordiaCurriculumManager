using ConcordiaCurriculumManager.DTO.CourseGrouping;
using NpgsqlTypes;

namespace ConcordiaCurriculumManager.Models.Curriculum.CourseGroupings;

public class CourseGroupingReference : BaseModel
{
    public required Guid ParentGroupId { get; set; }
    public required Guid ChildGroupCommonIdentifier { get; set; }
    public required GroupingTypeEnum GroupingType { get; set; }

    public static ICollection<CourseGroupingReference> CreateCourseGroupingReferencesFromDTO(Guid parentId, ICollection<CourseGroupingReferenceInputDTO> dto)
    {
        var references = new List<CourseGroupingReference>();
        foreach (var courseGroupingReference in dto)
        {
            references.Add(new CourseGroupingReference
            {
                Id = Guid.NewGuid(),
                ParentGroupId = parentId,
                ChildGroupCommonIdentifier = courseGroupingReference.ChildGroupCommonIdentifier,
                GroupingType = courseGroupingReference.GroupingType
            });
        }

        return references;
    }
}

public enum GroupingTypeEnum
{
    [PgName(nameof(SubGrouping))]
    SubGrouping,

    [PgName(nameof(OptionalGrouping))]
    OptionalGrouping,
}
