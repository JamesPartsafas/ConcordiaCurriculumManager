using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.OutputDTOs;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;

namespace ConcordiaCurriculumManager.DTO.Courses;

public class CourseChangesDTO
{
    public required IList<CourseCreationRequestCourseDetailsDTO> CourseCreationRequests { get; set; }
    public required IList<CourseModificationRequestCourseDetailsDTO> CourseModificationRequests { get; set; }
    public required IList<CourseDeletionRequestCourseDetailsDTO> CourseDeletionRequests { get; set; }
    public required IList<CourseDataDTO> OldCourses { get; set; }
}
