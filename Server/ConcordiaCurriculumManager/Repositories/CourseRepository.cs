using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.CourseGrouping;
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
    public Task<IList<Course>> GetCoursesByConcordiaCourseIds(IList<int> courseIds);
    public Task<Course?> GetCourseWithSupportingFilesBySubjectAndCatalog(string subject, string catalog);
    public Task<int?> GetCurrentCourseVersion(string subject, string catalog);
    public Task<Course?> GetPublishedVersion(string subject, string catalog);
    public Task<Course?> GetCourseInProposalBySubjectAndCatalog(string subject, string catalog);
    public Task<Course?> GetCourseByIdAsync(Guid id);
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
            && (course.CourseState == CourseStateEnum.Accepted || course.CourseState == CourseStateEnum.Deleted
            && course.Version != null))
        .OrderByDescending(course => course.Version)
        .Include(course => course.CourseCourseComponents)
        .Include(course => course.CourseCreationRequest)
        .Include(course => course.CourseModificationRequest)
        .Include(course => course.CourseDeletionRequest)
        .Include(course => course.CourseReferencing)
        .ThenInclude(cr => cr.CourseReferenced)
        .Include(course => course.CourseReferenced)
        .ThenInclude(cr => cr.CourseReferencing)
        .FirstOrDefaultAsync();

    public async Task<bool> SaveCourse(Course course)
    {
        course.VerifyCourseIsValidOrThrow();
        await _dbContext.Courses.AddAsync(course);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<Course?> GetCourseByCourseIdAndLatestVersion(int courseId) => await _dbContext.Courses
    .Where(course => 
        course.CourseID == courseId 
        && course.CourseState == CourseStateEnum.Accepted
        && course.Version != null)
    .OrderByDescending(course => course.Version)
    .Include(course => course.CourseCourseComponents)
    .FirstOrDefaultAsync();

    public async Task<IList<Course>> GetCoursesByConcordiaCourseIds(IList<int> courseIds)
    {
        if (courseIds.Count == 0)
            return new List<Course>();

        var query = _dbContext.Courses.FromSqlInterpolated(
                $@"SELECT DISTINCT ON (""CourseID"") c.* FROM ""Courses"" c WHERE ""Version"" IS NOT NULL ORDER BY ""CourseID"", ""Version"" DESC"
            );

        query = query.Where(course => courseIds.Contains(course.CourseID));
        query = query.Include(course => course.CourseCourseComponents);

        return await query.ToListAsync();
    }

    public async Task<Course?> GetCourseByCourseId(int courseId) => await _dbContext.Courses
        .Where(course => course.CourseID == courseId && course.CourseState == CourseStateEnum.Accepted)
        .OrderByDescending(course => course.Version)
        .Include(course => course.CourseCourseComponents)
        .Include(course => course.CourseCreationRequest)
        .Include(course => course.CourseModificationRequest)
        .Include(course => course.CourseDeletionRequest)
        .Include(course => course.CourseReferencing)
        .ThenInclude(cr => cr.CourseReferenced)
        .Include(course => course.CourseReferenced)
        .ThenInclude(cr => cr.CourseReferencing)
        .FirstOrDefaultAsync();

    public Task<Course?> GetCourseWithSupportingFilesBySubjectAndCatalog(string subject, string catalog) => _dbContext.Courses
        .Where(course =>
            course.Subject == subject
            && course.Catalog == catalog
            && (course.CourseState == CourseStateEnum.Accepted || course.CourseState == CourseStateEnum.Deleted
            && course.Version != null))
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

    public async Task<Course?> GetPublishedVersion(string subject, string catalog) => await _dbContext.Courses
        .Where(course =>
            course.Subject == subject
            && course.Catalog == catalog
            && course.Published)
        .Include(course => course.CourseCourseComponents)
        .FirstOrDefaultAsync();

    public async Task<Course?> GetCourseInProposalBySubjectAndCatalog(string subject, string catalog) => await _dbContext.Courses
    .Where(course =>
            course.Subject == subject
            && course.Catalog == catalog
            && course.CourseState == CourseStateEnum.NewCourseProposal)
    .FirstOrDefaultAsync();

    public async Task<Course?> GetCourseByIdAsync(Guid id)
    {
        return await _dbContext.Courses.FirstOrDefaultAsync(c => c.Id == id);
    }
}
