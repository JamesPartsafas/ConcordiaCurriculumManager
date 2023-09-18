using ConcordiaCurriculumManager.DTO;
using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Repositories;

namespace ConcordiaCurriculumManager.Services;

public interface ICourseService
{
    public IEnumerable<string> GetAllCourseCareers();
    public IEnumerable<CourseComponentDTO> GetAllCourseComponents();
    public Task<IEnumerable<string>> GetAllCourseSubjects();
}

public class CourseService : ICourseService
{
    private readonly ILogger<UserAuthenticationService> _logger;
    private readonly ICourseRepository _courseRepository;

    public CourseService(ILogger<UserAuthenticationService> logger, ICourseRepository courseRepository)
    {
        _logger = logger;
        _courseRepository = courseRepository;
    }

    public IEnumerable<string> GetAllCourseCareers()
    {
        return Enum.GetNames(typeof(CourseCareerEnum));
    }

    public IEnumerable<CourseComponentDTO> GetAllCourseComponents()
    {
        var componentsList = new List<CourseComponentDTO>();
        foreach (var mapping in ComponentCodeMapping.GetComponentCodeMapping().ToList())
        {
            componentsList.Add(new CourseComponentDTO { ComponentCode = mapping.Key, ComponentName = mapping.Value });
        }

        return componentsList;
    }

    public async Task<IEnumerable<string>> GetAllCourseSubjects()
    {
        return await _courseRepository.GetUniqueCourseSubjects();
    }
}
