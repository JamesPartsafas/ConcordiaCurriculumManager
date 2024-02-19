using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.CourseGroupings;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ConcordiaCurriculumManager.Repositories;

public interface ICourseGroupingRepository
{
    public Task<bool> SaveCourseGroupingRequest(CourseGroupingRequest courseGroupingRequest);
    public Task<bool> DeleteCourseGroupingRequest(CourseGroupingRequest courseGroupingRequest);
    public Task<CourseGroupingRequest?> GetCourseGroupingRequestById(Guid requestId);
    public Task<CourseGrouping?> GetCourseGroupingById(Guid groupingId);
    public Task<CourseGrouping?> GetCourseGroupingByCommonIdentifier(Guid commonId);
    public Task<ICollection<CourseGrouping>> GetCourseGroupingsBySchool(SchoolEnum school);
    public Task<ICollection<CourseGrouping>> GetCourseGroupingsLikeName(string name);
    public Task<IList<CourseGrouping>> GetCourseGroupingsContainingSubgrouping(CourseGrouping grouping);
    public Task<IList<CourseGrouping>> GetCourseGroupingsContainingCourse(Course course);
    public Task<CourseGrouping?> GetCourseGroupingContainingCourse(Course course);

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

    public async Task<bool> DeleteCourseGroupingRequest(CourseGroupingRequest courseGroupingRequest)
    {
        _dbContext.CourseGroupings.Remove(courseGroupingRequest.CourseGrouping!);
        _dbContext.CourseGroupingRequest.Remove(courseGroupingRequest);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<CourseGroupingRequest?> GetCourseGroupingRequestById(Guid requestId) => await _dbContext.CourseGroupingRequest
        .Where(x => x.Id.Equals(requestId))
        .Include(r => r.CourseGrouping)
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

    public async Task<ICollection<CourseGrouping>> GetCourseGroupingsBySchool(SchoolEnum school) => await _dbContext.CourseGroupings
        .Where(cg => cg.School.Equals(school) && cg.IsTopLevel)
        .Include(cg => cg.SubGroupingReferences)
        .Include(cg => cg.CourseIdentifiers)
        .ToListAsync();

    public async Task<ICollection<CourseGrouping>> GetCourseGroupingsLikeName(string name) => await _dbContext.CourseGroupings
        .OrderBy(cg => cg.Id)
        .Where(cg => cg.Name.ToLower().Contains(name.ToLower()))
        .Take(10)
        .Include(cg => cg.SubGroupingReferences)
        .Include(cg => cg.CourseIdentifiers)
        .ToListAsync();

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
