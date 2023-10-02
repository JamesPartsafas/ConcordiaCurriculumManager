using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ConcordiaCurriculumManager.Repositories;

public interface IDossierRepository
{
    Task<bool> SaveCourseCreationRequest(CourseCreationRequest courseCreationRequest);
    Task<List<Dossier>> GetDossiersByID(Guid userId);
    Task<Dossier?> GetDossierByDossierId(Guid dossierId);
    Task<bool> SaveDossier(Dossier dossier);
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

    public async Task<List<Dossier>> GetDossiersByID(Guid userId) {
        var dbDossiers = await _dbContext.Dossiers
            .Where(d => d.InitiatorId.Equals(userId))
            .ToListAsync();
        return dbDossiers;
            
    }

    public async Task<Dossier?> GetDossierByDossierId(Guid dossierId) => await _dbContext.Dossiers.Where(d => d.Id == dossierId).FirstOrDefaultAsync();

    public async Task<bool> SaveDossier(Dossier dossier)
    {
        await _dbContext.Dossiers.AddAsync(dossier);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }
}
