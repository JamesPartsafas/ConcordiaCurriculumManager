﻿using ConcordiaCurriculumManager.DTO.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ConcordiaCurriculumManager.Repositories;

public interface IDossierRepository
{
    Task<bool> SaveCourseCreationRequest(CourseCreationRequest courseCreationRequest);
    Task<bool> SaveCourseModificationRequest(CourseModificationRequest courseModificationRequest);
    Task<bool> SaveCourseDeletionRequest(CourseDeletionRequest courseDeletionRequest);
    Task<List<Dossier>> GetDossiersByID(Guid userId);
    Task<Dossier?> GetDossierByDossierId(Guid dossierId);
    Task<bool> SaveDossier(Dossier dossier);
    Task<bool> UpdateDossier(Dossier dossier);
    Task<bool> DeleteDossier(Dossier dossier);
    public Task<CourseCreationRequest?> GetCourseCreationRequest(Guid courseRequestId);
    public Task<bool> UpdateCourseCreationRequest(CourseCreationRequest courseCreationRequest);
    public Task<CourseModificationRequest?> GetCourseModificationRequest(Guid courseRequestId);
    public Task<bool> UpdateCourseModificationRequest(CourseModificationRequest courseModificatioRequest);
    public Task<CourseDeletionRequest?> GetCourseDeletionRequest(Guid courseRequestId);
    public Task<bool> UpdateCourseDeletionRequest(CourseDeletionRequest courseDeletionRequest);
    public Task<bool> DeleteCourseCreationRequest(CourseCreationRequest courseCreationRequest);
    public Task<bool> DeleteCourseModificationRequest(CourseModificationRequest courseModificationRequest);
    public Task<bool> DeleteCourseDeletionRequest(CourseDeletionRequest courseDeletionRequest);
    public Task<IList<User>> GetCurrentlyReviewingGroupMasters(Guid dossierId);
    public Task<Dossier?> GetDossierReportByDossierId(Guid dossierId);
    public Task<IList<Dossier>> GetDossiersRequiredReview(Guid userId);
    public Task<bool> CheckIfCourseRequestExists(Guid dossierId, string subject, string catalog);
    public Task<IList<Course>> GetChangesAcrossAllDossiers();
}

public class DossierRepository : IDossierRepository
{
    private readonly CCMDbContext _dbContext;

    public DossierRepository(CCMDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> SaveCourseCreationRequest(CourseCreationRequest courseCreationRequest)
    {
        await _dbContext.CourseCreationRequests.AddAsync(courseCreationRequest);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> SaveCourseModificationRequest(CourseModificationRequest courseModificationRequest)
    {
        await _dbContext.CourseModificationRequests.AddAsync(courseModificationRequest);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> SaveCourseDeletionRequest(CourseDeletionRequest courseDeletionRequest)
    {
        await _dbContext.CourseDeletionRequests.AddAsync(courseDeletionRequest);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<List<Dossier>> GetDossiersByID(Guid userId) {
        return await _dbContext.Dossiers
            .Where(d => d.InitiatorId.Equals(userId))
            .ToListAsync();
    }

    public async Task<Dossier?> GetDossierByDossierId(Guid dossierId) => await _dbContext.Dossiers
        .Where(d => d.Id == dossierId)
        .Include(d => d.CourseCreationRequests)
        .ThenInclude(c => c.NewCourse)
        .Include(d => d.CourseDeletionRequests)
        .ThenInclude(c => c.Course)
        .Include(d => d.CourseModificationRequests)
        .ThenInclude(c => c.Course)
        .Include(d => d.ApprovalStages)
        .ThenInclude(a => a.Group == null ? null : a.Group.Members)
        .Include(d => d.ApprovalStages)
        .ThenInclude(a => a.Group == null ? null : a.Group.GroupMasters)
        .Include(dossier => dossier.Discussion)
        .ThenInclude(discussion => discussion.Messages)
        .FirstOrDefaultAsync();

    public async Task<bool> SaveDossier(Dossier dossier)
    {
        await _dbContext.Dossiers.AddAsync(dossier);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> UpdateDossier(Dossier dossier) {
        _dbContext.Dossiers.Update(dossier);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> DeleteDossier(Dossier dossier) {
        _dbContext.Dossiers.Remove(dossier);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<CourseCreationRequest?> GetCourseCreationRequest(Guid courseRequestId)
    {
        var courseCreationRequest = await _dbContext.CourseCreationRequests.Where(c => c.Id == courseRequestId)
            .Include(cr => cr.NewCourse)
            .Include(cr => cr.NewCourse!.SupportingFiles)
            .Include(cr => cr.NewCourse!.CourseCourseComponents)
            .FirstOrDefaultAsync();
        return courseCreationRequest;
    }

    public async Task<bool> UpdateCourseCreationRequest(CourseCreationRequest courseCreationRequest)
    {
        _dbContext.CourseCreationRequests.Update(courseCreationRequest);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<CourseModificationRequest?> GetCourseModificationRequest(Guid courseRequestId)
    {
        var courseModificationRequest = await _dbContext.CourseModificationRequests.Where(c => c.Id == courseRequestId)
            .Include(cr => cr.Course)
            .Include(cr => cr.Course!.SupportingFiles)
            .Include(cr => cr.Course!.CourseCourseComponents)
            .FirstOrDefaultAsync();
        return courseModificationRequest;
    }

    public async Task<bool> UpdateCourseModificationRequest(CourseModificationRequest courseModificatioRequest)
    {
        _dbContext.CourseModificationRequests.Update(courseModificatioRequest);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<CourseDeletionRequest?> GetCourseDeletionRequest(Guid courseRequestId)
    {
        var courseDeletionRequest = await _dbContext.CourseDeletionRequests.Where(c => c.Id == courseRequestId).Include(cr => cr.Course).FirstOrDefaultAsync();
        return courseDeletionRequest;
    }

    public async Task<bool> UpdateCourseDeletionRequest(CourseDeletionRequest courseDeletionRequest)
    {
        _dbContext.CourseDeletionRequests.Update(courseDeletionRequest);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> DeleteCourseCreationRequest(CourseCreationRequest courseCreationRequest)
    {
        _dbContext.Courses.Remove(courseCreationRequest.NewCourse!);
        _dbContext.CourseCreationRequests.Remove(courseCreationRequest);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> DeleteCourseModificationRequest(CourseModificationRequest courseModificationRequest)
    {
        _dbContext.Courses.Remove(courseModificationRequest.Course!);
        _dbContext.CourseModificationRequests.Remove(courseModificationRequest);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> DeleteCourseDeletionRequest(CourseDeletionRequest courseDeletionRequest)
    {
        _dbContext.Courses.Remove(courseDeletionRequest.Course!);
        _dbContext.CourseDeletionRequests.Remove(courseDeletionRequest);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<IList<User>> GetCurrentlyReviewingGroupMasters(Guid dossierId)
    {
        var stage = await _dbContext.ApprovalStages
            .Where(stage => stage.DossierId.Equals(dossierId) && stage.IsCurrentStage)
            .Include(stage => stage.Group)
            .ThenInclude(group => group!.GroupMasters)
            .FirstOrDefaultAsync();

        if (stage == null || stage.Group == null)
            return new List<User>();

        return stage.Group.GroupMasters;
    }

    public async Task<Dossier?> GetDossierReportByDossierId(Guid dossierId)
    {
        return await _dbContext.Dossiers
        .Where(d => d.Id == dossierId)
        .Include(d => d.Initiator)
        .Include(d => d.CourseCreationRequests)
            .ThenInclude(ccr => ccr.NewCourse)
                .ThenInclude(nc => nc!.SupportingFiles)
            .Include(d => d.CourseCreationRequests)
                .ThenInclude(ccr => ccr.NewCourse)
                    .ThenInclude(nc => nc!.CourseCourseComponents)
        .Include(d => d.CourseModificationRequests)
            .ThenInclude(cmr => cmr.Course)
                .ThenInclude(cr => cr!.SupportingFiles)
            .Include(d => d.CourseModificationRequests)
                .ThenInclude(cmr => cmr.Course)
                    .ThenInclude(cr => cr!.CourseCourseComponents)
        .Include(d => d.CourseDeletionRequests)
            .ThenInclude(cmr => cmr.Course)
                .ThenInclude(cr => cr!.SupportingFiles)
            .Include(d => d.CourseModificationRequests)
                .ThenInclude(cmr => cmr.Course)
                    .ThenInclude(cr => cr!.CourseCourseComponents)
        .Include(d => d.ApprovalStages)
            .ThenInclude(a => a.Group)
        .FirstOrDefaultAsync();
    }

    public async Task<IList<Dossier>> GetDossiersRequiredReview(Guid userId)
    {
        return await _dbContext.Dossiers
            .Include(d => d.ApprovalStages)
            .ThenInclude(a => a.Group)
            .Where(d => d.ApprovalStages.Where(a => a.IsCurrentStage).First().Group!.Members.Any(m => m.Id.Equals(userId)))
            .ToListAsync();
    }

    public async Task<bool> CheckIfCourseRequestExists(Guid dossierId, string subject, string catalog)
    {
       var dossier = await GetDossierByDossierId(dossierId);

       var courseRequests = dossier!.CourseCreationRequests.Select(r => r.NewCourse)
                            .Concat(dossier.CourseModificationRequests.Select(r => r.Course))
                            .Concat(dossier.CourseDeletionRequests.Select(r => r.Course))
                            .Where(c => c != null);
       
        foreach (var course in courseRequests)
       {
            if (course!.Subject.Equals(subject) && course.Catalog.Equals(catalog)) 
            {
                return true;
            }
       }
       return false;
    }

    public async Task<IList<Course>> GetChangesAcrossAllDossiers()
    {
        var query = _dbContext.Courses.FromSqlInterpolated(
                $@"SELECT DISTINCT ON (""CourseID"") c.* FROM ""Courses"" c WHERE ""Version"" IS NOT NULL AND ""Published"" = false ORDER BY ""CourseID"", ""Version"" DESC"
            );

        query = query.Include(course => course.CourseCourseComponents)
            .Include(course => course.CourseCreationRequest)
            .Include(course => course.CourseModificationRequest)
            .Include(course => course.CourseDeletionRequest);

        return await query.ToListAsync();
    }
}
