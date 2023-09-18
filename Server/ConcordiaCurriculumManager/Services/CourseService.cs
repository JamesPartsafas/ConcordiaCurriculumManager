using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Repositories;

namespace ConcordiaCurriculumManager.Services;

public interface ICourseService
{
    public Task<IEnumerable<CourseComponent>> GetCourseComponents();
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

    public async Task<IEnumerable<CourseComponent>> GetCourseComponents()
    {
        return await _courseRepository.GetAllCourseComponents();
    }
}
