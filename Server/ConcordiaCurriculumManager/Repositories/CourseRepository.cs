﻿using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ConcordiaCurriculumManager.Repositories;

public interface ICourseRepository
{
    public Task<List<string>> GetUniqueCourseSubjects();
    public Task<int> GetMaxCourseId();
    public Task<Course?> GetCourseBySubjectAndCatalog(string subject, string catalog);
    public Task<bool> SaveCourse(Course course);
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
        .Where(course => course.Subject == subject && course.Catalog == catalog).FirstOrDefaultAsync();

    public async Task<bool> SaveCourse(Course course)
    {
        await _dbContext.Courses.AddAsync(course);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }
}