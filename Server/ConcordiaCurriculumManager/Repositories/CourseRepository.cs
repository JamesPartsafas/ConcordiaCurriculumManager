using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ConcordiaCurriculumManager.Repositories;

public interface ICourseRepository
{
    public Task<List<string>> GetUniqueCourseSubjects();
    public Task<int> GetMaxCourseId();
    public Task<Course?> GetCourseBySubjectAndCatalog(string subject, string catalog);
    public Task<bool> SaveCourse(Course course);
    public Task<Course?> GetCourseByCourseId(int courseId);
    public Task<Course?> GetCourseByCourseIdAndLatestVersion(int courseId);
    public Task<Course?> GetCourseWithSupportingFilesBySubjectAndCatalog(string subject, string catalog);
    public Task<int?> GetCurrentCourseVersion(string subject, string catalog);
}

public class CourseRepository : ICourseRepository
{
    private readonly CCMDbContext _dbContext;

    public CourseRepository(CCMDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<string>> GetUniqueCourseSubjects() => _dbContext.Courses.Select(course => course.Subject).Distinct().ToListAsync();

    public async Task<int> GetMaxCourseId() => (await _dbContext.Courses.MaxAsync(course => (int?)course.CourseID)) ?? 0;

    public Task<Course?> GetCourseBySubjectAndCatalog(string subject, string catalog) => _dbContext.Courses
        .Where(course => 
            course.Subject == subject 
            && course.Catalog == catalog 
            && (course.CourseState == CourseStateEnum.Accepted || course.CourseState == CourseStateEnum.Deleted))
        .OrderByDescending(course => course.Version)
        .Select(ObjectSelectors.CourseSelector())
        .FirstOrDefaultAsync();

    public async Task<bool> SaveCourse(Course course)
    {
        course.VerifyCourseIsValidOrThrow();
        await _dbContext.Courses.AddAsync(course);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public Task<Course?> GetCourseByCourseIdAndLatestVersion(int courseId) => _dbContext.Courses
    .Where(course => 
        course.CourseID == courseId 
        && course.CourseState == CourseStateEnum.Accepted)
    .OrderByDescending(course => course.Version)
    .Select(ObjectSelectors.CourseSelector())
    .FirstOrDefaultAsync();

    public async Task<Course?> GetCourseByCourseId(int courseId) => await _dbContext.Courses
        .Where(course => course.CourseID == courseId && course.CourseState == CourseStateEnum.Accepted)
        .OrderByDescending(course => course.Version)
        .Select(ObjectSelectors.CourseSelector())
        .FirstOrDefaultAsync();

    public Task<Course?> GetCourseWithSupportingFilesBySubjectAndCatalog(string subject, string catalog) => _dbContext.Courses
        .Where(course =>
            course.Subject == subject
            && course.Catalog == catalog
            && (course.CourseState == CourseStateEnum.Accepted || course.CourseState == CourseStateEnum.Deleted))
        .Include(course => course.SupportingFiles)
        .Include(course => course.CourseCourseComponents)
        .OrderByDescending(course => course.Version)
        .FirstOrDefaultAsync();

    public async Task<int?> GetCurrentCourseVersion(string subject, string catalog) => await _dbContext.Courses
        .Where(course =>
            course.Subject == subject
            && course.Catalog == catalog
            && course.Version != null)
        .OrderByDescending(course => course.Version)
        .Select(course => course.Version)
        .FirstOrDefaultAsync();
}
