﻿using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.CourseGrouping;
using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ConcordiaCurriculumManager.Repositories;

public interface ICourseGroupingRepository
{
    public Task<CourseGrouping?> GetCourseGroupingById(Guid groupingId);
    public Task<CourseGrouping?> GetCourseGroupingByCommonIdentifier(Guid commonId);
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
}
