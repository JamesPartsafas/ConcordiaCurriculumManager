using ConcordiaCurriculumManager.Models.Curriculum;

namespace ConcordiaCurriculumManager.DTO.Courses;

public class CourseCareerDTO
{
    public required CourseCareerEnum CareerCode { get; set; }

    public required string CareerName { get; set; }
}
