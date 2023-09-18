using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ConcordiaCurriculumManager.Repositories;

public interface ICourseRepository
{
    public Task<List<CourseComponent>> GetAllCourseComponents();
}

public class CourseRepository : ICourseRepository
{
    private readonly CCMDbContext _dbContext;

    public CourseRepository(CCMDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<CourseComponent>> GetAllCourseComponents() => _dbContext.CourseComponents.ToListAsync();
}
