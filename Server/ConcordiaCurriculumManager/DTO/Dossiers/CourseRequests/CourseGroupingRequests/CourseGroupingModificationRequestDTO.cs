using ConcordiaCurriculumManager.DTO.CourseGrouping;

namespace ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.CourseGroupingRequests;

public class CourseGroupingModificationRequestDTO : CourseInitiationDTO
{
    public required CourseGroupingModificationInputDTO CourseGrouping { get; set; }
}
