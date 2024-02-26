using ConcordiaCurriculumManager.DTO.CourseGrouping;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;

namespace ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.CourseGroupingRequests;

public class CourseGroupingRequestDTO : CourseRequestDTO
{
    public required CourseGroupingDTO CourseGrouping { get; set; }
    public required RequestType RequestType { get; set; }
}
