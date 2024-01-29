using ConcordiaCurriculumManager.DTO.CourseGrouping;

namespace ConcordiaCurriculumManager.Models.Curriculum.CourseGroupings;

public class CourseIdentifier : BaseModel
{
    public required int ConcordiaCourseId { get; set; }

    public static ICollection<CourseIdentifier> CreateCourseIdentifiersFromDTO(ICollection<CourseIdentifierDTO> dto)
    {
        var identifiers = new List<CourseIdentifier>();
        foreach (var courseIdentifier in dto)
        {
            identifiers.Add(new CourseIdentifier
            {
                ConcordiaCourseId = courseIdentifier.ConcordiaCourseId
            });
        }

        return identifiers;
    }
}
