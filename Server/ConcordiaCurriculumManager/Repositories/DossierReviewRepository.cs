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
    Task<Dossier?> GetDossierWithApprovalStagesAndRequests(Guid dossierId);
    Task<Dossier?> GetDossierWithApprovalStagesAndRequestsAndDiscussion(Guid dossierId);
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
        await _dbContext.ApprovalStages.AddRangeAsync(stages);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<Dossier?> GetDossierWithApprovalStages(Guid dossierId)
    {
        return await _dbContext.Dossiers
            .Where(dossier => dossier.Id.Equals(dossierId))
            .Include(dossier => dossier.ApprovalStages)
            .Include(dossier => dossier.ApprovalHistories)
            .FirstOrDefaultAsync();
    }

    public async Task<Dossier?> GetDossierWithApprovalStagesAndRequests(Guid dossierId)
    {
        return await _dbContext.Dossiers
            .Where(dossier => dossier.Id.Equals(dossierId))
            .Include(dossier => dossier.Initiator)
            .Include(dossier => dossier.ApprovalStages)
            .Include(dossier => dossier.ApprovalHistories)
            .Include(dossier => dossier.CourseCreationRequests)
            .ThenInclude(creationRequests => creationRequests.NewCourse)
            .Include(dossier => dossier.CourseModificationRequests)
            .ThenInclude(modificationRequests => modificationRequests.Course)
            .Include(dossier => dossier.CourseDeletionRequests)
            .ThenInclude(deletionRequests => deletionRequests.Course)
            .Include(dossier => dossier.CourseGroupingRequests)
            .ThenInclude(cgr => cgr.CourseGrouping)
            .ThenInclude(cg => cg!.CourseIdentifiers)
            .Include(dossier => dossier.CourseGroupingRequests)
            .ThenInclude(cgr => cgr.CourseGrouping)
            .ThenInclude(cg => cg!.SubGroupings)
            .Include(dossier => dossier.CourseGroupingRequests)
            .ThenInclude(cgr => cgr.CourseGrouping)
            .ThenInclude(cg => cg!.SubGroupingReferences)
            .FirstOrDefaultAsync();
    }

    public async Task<Dossier?> GetDossierWithApprovalStagesAndRequestsAndDiscussion(Guid dossierId)
    {
        return await _dbContext.Dossiers
            .Where(dossier => dossier.Id.Equals(dossierId))
            .Include(dossier => dossier.ApprovalStages)
            .Include(dossier => dossier.ApprovalHistories)
            .Include(dossier => dossier.CourseCreationRequests)
            .ThenInclude(creationRequests => creationRequests.NewCourse)
            .Include(dossier => dossier.CourseModificationRequests)
            .ThenInclude(modificationRequests => modificationRequests.Course)
            .Include(dossier => dossier.CourseDeletionRequests)
            .ThenInclude(deletionRequests => deletionRequests.Course)
            .Include(dossier => dossier.CourseGroupingRequests)
            .ThenInclude(cgr => cgr.CourseGrouping)
            .ThenInclude(cg => cg!.CourseIdentifiers)
            .Include(dossier => dossier.CourseGroupingRequests)
            .ThenInclude(cgr => cgr.CourseGrouping)
            .ThenInclude(cg => cg!.SubGroupings)
            .Include(dossier => dossier.CourseGroupingRequests)
            .ThenInclude(cgr => cgr.CourseGrouping)
            .ThenInclude(cg => cg!.SubGroupingReferences)
            .Include(dossier => dossier.Discussion)
            .ThenInclude(discussion => discussion == null ? null : discussion.Messages)
            .FirstOrDefaultAsync();
    }
}
