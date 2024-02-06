using ConcordiaCurriculumManager.DTO.CourseGrouping;

namespace ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.CourseGroupingRequests;

public class CourseGroupingCreationRequestDTO : CourseInitiationDTO
{
    public required CourseGroupingInputDTO CourseGrouping { get; set; }
}
