using ConcordiaCurriculumManager.DTO.Courses;

namespace ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.OutputDTOs;

public class CourseDeletionRequestCourseDetailsDTO : CourseDeletionRequestDTO
{
    public required CourseDataDTO Course { get; set; }
}

