using ConcordiaCurriculumManager.Models.Curriculum;
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
    public Task<List<Course>> GetCoursesBySubjectAsync(string subjectCode);
    public Task<bool> UpdateCourse(Course course);
    Task<bool> UpdateCourseReferences(Course oldCourse, Course newCourse, List<(string Subject, string Catalog)> newCourseReferencing);
    Task<bool> InvalidateAllCourseReferences(Guid id);
    Task<bool> AddCourseReferences(Course newCourse, List<(string Subject, string Catalog)> newCourseReferenced);
    Task<IEnumerable<Course>> GetCourseHistory(string subject, string catalog);
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

    public async Task<IEnumerable<Course>> GetCourseHistory(string subject, string catalog)
    {
        var query = from course in _dbContext.Courses
                    where course.Subject == subject
                          && course.Catalog == catalog
                          && (course.CourseState == CourseStateEnum.Accepted || course.CourseState == CourseStateEnum.Deleted)
                    let latestPublishedVersion = _dbContext.Courses
                                                   .Where(c => c.Subject == subject && c.Catalog == catalog && c.Published)
                                                   .Max(c => c.Version)
                    where course.Version <= latestPublishedVersion
                    orderby course.Version descending
                    select course;

        return await query.ToListAsync();
    }

    public async Task<Course?> GetCourseInProposalBySubjectAndCatalog(string subject, string catalog) => await _dbContext.Courses
    .Where(course =>
            course.Subject == subject
            && course.Catalog == catalog
            && course.CourseState == CourseStateEnum.NewCourseProposal)
    .FirstOrDefaultAsync();

    public async Task<Course?> GetCourseByIdAsync(Guid id)
    {
        return await _dbContext.Courses
        .Include(c => c.CourseCourseComponents)
        .Include(c => c.SupportingFiles)
        .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Course>> GetCoursesBySubjectAsync(string subjectCode)
    {
        return await _dbContext.Courses
            .Where(c => c.Subject == subjectCode)
            .Include(c => c.CourseCourseComponents)
            .Include(c => c.SupportingFiles)
            .ToListAsync();
    }

    public async Task<bool> UpdateCourse(Course course)
    {
        _dbContext.Courses.Update(course);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> UpdateCourseReferences(Course oldCourse, Course newCourse, List<(string Subject, string Catalog)> newCourseReferenced)
    {
        using var transaction = _dbContext.Database.BeginTransaction();

        var references = await _dbContext.CourseReferences
                .Where(c => c.State.Equals(CourseReferenceEnum.UpToDate) && (c.CourseReferencedId.Equals(oldCourse.Id) || c.CourseReferencingId.Equals(oldCourse.Id)))
                .ToListAsync();

        references.ForEach(reference => reference.State = CourseReferenceEnum.OutOfDate);

        var newReferences = references.Where(reference => reference.CourseReferencedId.Equals(oldCourse.Id)).Select(reference => new CourseReference
        {
            CourseReferencedId = newCourse.Id,
            CourseReferenced = newCourse,
            CourseReferencingId = reference.CourseReferencingId,
            CourseReferencing = reference.CourseReferencing,
            State = CourseReferenceEnum.UpToDate
        }).ToList();

        foreach ((var subject, var catalog) in newCourseReferenced)
        {
            var course = await GetPublishedVersion(subject, catalog);

            if (course is null)
            {
                continue;
            }

            newReferences.Add(new CourseReference
            {
                CourseReferencedId = course.Id,
                CourseReferenced = course,
                CourseReferencingId = newCourse.Id,
                CourseReferencing = newCourse,
                State = CourseReferenceEnum.UpToDate
            });
        }

        _dbContext.CourseReferences.UpdateRange(references);
        await _dbContext.CourseReferences.AddRangeAsync(newReferences);
        var result = await _dbContext.SaveChangesAsync();
        await transaction.CommitAsync();
        return result == (newReferences.Count + references.Count);
    }

    public async Task<bool> InvalidateAllCourseReferences(Guid id)
    {
        using var transaction = _dbContext.Database.BeginTransaction();

        var references = await _dbContext.CourseReferences
                .Where(c => c.State.Equals(CourseReferenceEnum.UpToDate) && (c.CourseReferencedId.Equals(id) || c.CourseReferencingId.Equals(id)))
                .ToListAsync();

        references.ForEach(reference => reference.State = CourseReferenceEnum.OutOfDate);

        _dbContext.CourseReferences.UpdateRange(references);
        var result = await _dbContext.SaveChangesAsync();
        await transaction.CommitAsync();
        return result == references.Count;
    }

    public async Task<bool> AddCourseReferences(Course newCourse, List<(string Subject, string Catalog)> newCourseReferenced)
    {
        using var transaction = _dbContext.Database.BeginTransaction();

        var newReferences = new List<CourseReference>();
        foreach ((var subject, var catalog) in newCourseReferenced)
        {
            var course = await GetPublishedVersion(subject, catalog);

            if (course is null)
            {
                continue;
            }

            newReferences.Add(new CourseReference
            {
                CourseReferencedId = course.Id,
                CourseReferenced = course,
                CourseReferencingId = newCourse.Id,
                CourseReferencing = newCourse,
                State = CourseReferenceEnum.UpToDate
            });
        }

        await _dbContext.CourseReferences.AddRangeAsync(newReferences);
        var result = await _dbContext.SaveChangesAsync();
        await transaction.CommitAsync();
        return (result == newReferences.Count);
    }
}
