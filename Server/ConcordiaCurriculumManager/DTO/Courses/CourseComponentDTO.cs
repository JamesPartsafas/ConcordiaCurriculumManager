using ConcordiaCurriculumManager.Models.Curriculum;

namespace ConcordiaCurriculumManager.DTO.Courses;

public class CourseComponentDTO
{
    public required ComponentCodeEnum ComponentCode { get; set; }

    public required string ComponentName { get; set; }
}
