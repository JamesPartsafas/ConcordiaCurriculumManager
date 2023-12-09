using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace ConcordiaCurriculumManager.Repositories;

public interface IDossierReviewRepository
{
    Task<bool> SaveApprovalStages(IList<ApprovalStage> stages);
    Task<Dossier?> GetDossierWithApprovalStages(Guid dossierId);
}

public class DossierReviewRepository : IDossierReviewRepository
{
    private readonly CCMDbContext _dbContext;

    public DossierReviewRepository(CCMDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> SaveApprovalStages(IList<ApprovalStage> stages)
    {
        _dbContext.BulkInsert(stages);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<Dossier?> GetDossierWithApprovalStages(Guid dossierId)
    {
        return await _dbContext.Dossiers
            .Where(dossier => dossier.Id.Equals(dossierId))
            .Include(dossier => dossier.ApprovalStages)
            .FirstOrDefaultAsync();
    }
}
