using ConcordiaCurriculumManager.DTO.CourseGrouping;

namespace ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.CourseGroupingRequests;

public class CourseGroupingRequestDTO : CourseRequestDTO
{
    public required CourseGroupingDTO CourseGrouping { get; set; }
}
