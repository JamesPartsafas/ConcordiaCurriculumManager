using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.CourseGroupings;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ConcordiaCurriculumManager.Repositories;

public interface ICourseGroupingRepository
{
    public Task<bool> SaveCourseGroupingRequest(CourseGroupingRequest courseGroupingRequest);
    public Task<bool> DeleteCourseGroupingRequest(CourseGroupingRequest courseGroupingRequest);
    public Task<bool> DeleteSubgrouping(CourseGroupingReference reference);
    public Task<CourseGroupingRequest?> GetCourseGroupingRequestById(Guid requestId);
    public Task<CourseGrouping?> GetCourseGroupingById(Guid groupingId);
    public Task<CourseGrouping?> GetCourseGroupingByCommonIdentifier(Guid commonId);
    public Task<ICollection<CourseGrouping>> GetCourseGroupingsBySchool(SchoolEnum school);
    public Task<ICollection<CourseGrouping>> GetCourseGroupingsLikeName(string name);
    public Task<IList<CourseGrouping>> GetCourseGroupingsContainingSubgrouping(CourseGrouping grouping);
    public Task<IList<CourseGrouping>> GetCourseGroupingsContainingCourse(Course course);
    public Task<CourseGrouping?> GetCourseGroupingContainingCourse(Course course);
    public Task<CourseGrouping?> GetPublishedVersion(Guid commonId);
    public Task<bool> UpdateCourseGrouping(CourseGrouping courseGrouping);
    public Task<CourseGrouping?> GetCourseGroupingByCommonIdentifierAnyState(Guid commonId);
}

public class CourseGroupingRepository : ICourseGroupingRepository
{
    private readonly CCMDbContext _dbContext;

    public CourseGroupingRepository(CCMDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> SaveCourseGroupingRequest(CourseGroupingRequest courseGroupingRequest)
    {
        await _dbContext.CourseGroupingRequest.AddAsync(courseGroupingRequest);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> UpdateCourseGrouping(CourseGrouping courseGrouping)
    {
        _dbContext.CourseGroupings.Update(courseGrouping);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> DeleteCourseGroupingRequest(CourseGroupingRequest courseGroupingRequest)
    {
        _dbContext.CourseGroupings.Remove(courseGroupingRequest.CourseGrouping!);
        _dbContext.CourseGroupingRequest.Remove(courseGroupingRequest);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> DeleteSubgrouping(CourseGroupingReference reference)
    {
        _dbContext.CourseGroupingReferences.Remove(reference);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<CourseGroupingRequest?> GetCourseGroupingRequestById(Guid requestId) => await _dbContext.CourseGroupingRequest
        .Where(x => x.Id.Equals(requestId))
        .Include(r => r.CourseGrouping)
        .ThenInclude(cg => cg!.CourseIdentifiers)
        .Include(r => r.CourseGrouping)
        .ThenInclude(cg => cg!.SubGroupingReferences)
        .FirstOrDefaultAsync();

    public async Task<CourseGrouping?> GetCourseGroupingById(Guid groupingId) => await _dbContext.CourseGroupings
        .Where(cg => cg.Id.Equals(groupingId))
        .Include(cg => cg.SubGroupingReferences)
        .Include(cg => cg.CourseIdentifiers)
        .FirstOrDefaultAsync();

    public async Task<CourseGrouping?> GetCourseGroupingByCommonIdentifier(Guid commonId) => await _dbContext.CourseGroupings
        .Where(cg => cg.CommonIdentifier.Equals(commonId)
            && cg.State == CourseGroupingStateEnum.Accepted
            && cg.Version != null)
        .Include(cg => cg.SubGroupingReferences)
        .Include(cg => cg.CourseIdentifiers)
        .OrderByDescending(cg => cg.Version)
        .FirstOrDefaultAsync();

    public async Task<CourseGrouping?> GetCourseGroupingByCommonIdentifierAnyState(Guid commonId) => await _dbContext.CourseGroupings
        .Where(cg => cg.CommonIdentifier.Equals(commonId) && cg.Version != null)
        .Include(cg => cg.SubGroupingReferences)
        .Include(cg => cg.CourseIdentifiers)
        .OrderByDescending(cg => cg.Version)
        .FirstOrDefaultAsync();

    public async Task<CourseGrouping?> GetPublishedVersion(Guid commonId) => await _dbContext.CourseGroupings
        .Where(cg => cg.CommonIdentifier.Equals(commonId) && cg.Published)
        .Include(cg => cg.SubGroupingReferences)
        .Include(cg => cg.CourseIdentifiers)
        .FirstOrDefaultAsync();

    public async Task<ICollection<CourseGrouping>> GetCourseGroupingsBySchool(SchoolEnum school)
    {
        var result = await _dbContext.CourseGroupings
        .Where(cg => cg.School.Equals(school) && cg.IsTopLevel && cg.Version != null)
        .Include(cg => cg.SubGroupingReferences)
        .Include(cg => cg.CourseIdentifiers)
        .GroupBy(cg => cg.CommonIdentifier)
        .Select(group => group.OrderByDescending(cg => cg.Version).First())
        .ToListAsync();

        return result.Where(cg => cg.State.Equals(CourseGroupingStateEnum.Accepted)).ToList();
    }

    public async Task<ICollection<CourseGrouping>> GetCourseGroupingsLikeName(string name)
    {
        var result =  await _dbContext.CourseGroupings
        .Where(cg => cg.Name.Trim().ToLower().Contains(name.Trim().ToLower()) && cg.Version != null)
        .Take(10)
        .Include(cg => cg.SubGroupingReferences)
        .Include(cg => cg.CourseIdentifiers)
        .GroupBy(cg => cg.CommonIdentifier)
        .Select(group => group.OrderByDescending(cg => cg.Version).First())
        .ToListAsync();

        return result.Where(cg => cg.State.Equals(CourseGroupingStateEnum.Accepted)).ToList();
    }

    public async Task<IList<CourseGrouping>> GetCourseGroupingsContainingSubgrouping(CourseGrouping grouping)
    {
        var query = _dbContext.CourseGroupings.FromSqlInterpolated(
                $@"SELECT DISTINCT ON (""CommonIdentifier"") c.* FROM ""CourseGroupings"" c WHERE ""Version"" IS NOT NULL ORDER BY ""CommonIdentifier"", ""Version"" DESC"
            );

        query = query.Include(g => g.SubGroupingReferences);
        query = query.Where(g => g.SubGroupingReferences.Any(reference => reference.ChildGroupCommonIdentifier.Equals(grouping.CommonIdentifier)));

        return await query.ToListAsync();
    }

    public async Task<IList<CourseGrouping>> GetCourseGroupingsContainingCourse(Course course)
    {
        var query = _dbContext.CourseGroupings.FromSqlInterpolated(
                $@"SELECT DISTINCT ON (""CommonIdentifier"") c.* FROM ""CourseGroupings"" c WHERE ""Version"" IS NOT NULL ORDER BY ""CommonIdentifier"", ""Version"" DESC"
            );

        query = query.Include(cg => cg.CourseIdentifiers);
        query = query.Where(cg => cg.CourseIdentifiers.Any(ci => ci.ConcordiaCourseId.Equals(course.CourseID)));

        return await query.ToListAsync();
    }

    public async Task<CourseGrouping?> GetCourseGroupingContainingCourse(Course course) => await _dbContext.CourseGroupings
       .Include(cg => cg.CourseIdentifiers)
       .Where(cg => cg.CourseIdentifiers.Any(ci => ci.ConcordiaCourseId.Equals(course.CourseID)))
       .FirstOrDefaultAsync();
}
