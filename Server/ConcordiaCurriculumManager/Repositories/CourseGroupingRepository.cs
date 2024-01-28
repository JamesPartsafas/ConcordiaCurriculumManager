using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.CourseGroupings;
using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ConcordiaCurriculumManager.Repositories;

public interface ICourseGroupingRepository
{
    public Task<CourseGrouping?> GetCourseGroupingById(Guid groupingId);
    public Task<CourseGrouping?> GetCourseGroupingByCommonIdentifier(Guid commonId);
    public Task<ICollection<CourseGrouping>> GetCourseGroupingsBySchool(SchoolEnum school);
    public Task<ICollection<CourseGrouping>> GetCourseGroupingsLikeName(string name);
    public Task<CourseGrouping?> GetCourseGroupingContainingCourse(Course course);
}

public class CourseGroupingRepository : ICourseGroupingRepository
{
    private readonly CCMDbContext _dbContext;

    public CourseGroupingRepository(CCMDbContext dbContext)
    {
        _dbContext = dbContext;
    }

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

    public async Task<CourseGrouping?> GetCourseGroupingContainingCourse(Course course) => await _dbContext.CourseGroupings
        .Include(cg => cg.CourseIdentifiers)
        .Where(cg => cg.CourseIdentifiers.Any(ci => ci.ConcordiaCourseId.Equals(course.CourseID)))
        .FirstOrDefaultAsync();
}
