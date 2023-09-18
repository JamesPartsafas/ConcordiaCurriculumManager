namespace ConcordiaCurriculumManager.DTO;

public class CourseSettingsDTO
{
    public required IEnumerable<CourseComponentDTO> CourseComponents { get; set; }

    public required IEnumerable<string> CourseCareers { get; set; }

    public required IEnumerable<string> CourseSubjects {  get; set; }
}
