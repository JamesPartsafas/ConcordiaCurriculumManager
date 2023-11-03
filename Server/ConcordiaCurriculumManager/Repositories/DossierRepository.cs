using ConcordiaCurriculumManager.DTO.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
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
    public Task<bool> DeleteCourseCreationRequest(CourseCreationRequest courseCreationRequest);
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
        var dbDossiers = await _dbContext.Dossiers
            .Where(d => d.InitiatorId.Equals(userId))
            .ToListAsync();
        return dbDossiers;
            
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
        var courseCreationRequest = await _dbContext.CourseCreationRequests.Where(c => c.Id == courseRequestId).Include(cr => cr.NewCourse).FirstOrDefaultAsync();
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
        var courseModificationRequest = await _dbContext.CourseModificationRequests.Where(c => c.Id == courseRequestId).Include(cr => cr.Course).FirstOrDefaultAsync();
        return courseModificationRequest;
    }

    public async Task<bool> UpdateCourseModificationRequest(CourseModificationRequest courseModificatioRequest)
    {
        _dbContext.CourseModificationRequests.Update(courseModificatioRequest);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> DeleteCourseCreationRequest(CourseCreationRequest courseCreationRequest)
    {
        _dbContext.CourseCreationRequests.Remove(courseCreationRequest);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }
}
