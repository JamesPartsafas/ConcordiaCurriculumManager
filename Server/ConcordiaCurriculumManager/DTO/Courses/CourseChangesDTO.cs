using ConcordiaCurriculumManager.DTO.CourseGrouping;
using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.CourseGroupingRequests;
using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.OutputDTOs;

namespace ConcordiaCurriculumManager.DTO.Courses;

public class CourseChangesDTO
{
    public required IList<CourseCreationRequestCourseDetailsDTO> CourseCreationRequests { get; set; }
    public required IList<CourseModificationRequestCourseDetailsDTO> CourseModificationRequests { get; set; }
    public required IList<CourseDeletionRequestCourseDetailsDTO> CourseDeletionRequests { get; set; }
    public required IList<CourseGroupingRequestDTO> CourseGroupingRequests { get; set; }
    public required IList<CourseDataDTO> OldCourses { get; set; }
    public required IList<CourseGroupingDTO> OldCourseGroupings { get; set; }
}
