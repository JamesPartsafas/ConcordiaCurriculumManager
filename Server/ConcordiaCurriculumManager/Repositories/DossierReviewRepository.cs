using ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using EFCore.BulkExtensions;

namespace ConcordiaCurriculumManager.Repositories;

public interface IDossierReviewRepository
{
    Task<bool> SaveApprovalStages(IList<ApprovalStage> stages);
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
}
