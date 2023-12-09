using ConcordiaCurriculumManager.DTO.Dossiers;
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
        .Select(ObjectSelectors.DossierSelector())
        .Where(d => d.Id == dossierId)
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

        return stage!.Group!.GroupMasters;
    }
}
