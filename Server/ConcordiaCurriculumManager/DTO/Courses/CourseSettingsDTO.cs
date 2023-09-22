namespace ConcordiaCurriculumManager.DTO.Courses;

public class CourseSettingsDTO
{
    public required IEnumerable<CourseComponentDTO> CourseComponents { get; set; }

    public required IEnumerable<CourseCareerDTO> CourseCareers { get; set; }

    public required IEnumerable<string> CourseSubjects { get; set; }
}
