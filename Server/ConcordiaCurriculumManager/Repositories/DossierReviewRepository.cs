using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Models.Users;
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
    Task<DiscussionMessage?> GetDiscussionMessageWithId(Guid dicussionMessageId);
    Task<bool> UpdateDiscussionMessageReview(DiscussionMessage dossier);
    Task<DiscussionMessageVote?> GetVoteByUserAndMessageId(Guid userId, Guid messageId);
    Task<bool> InsertVote(DiscussionMessageVote vote);
    Task<bool> DeleteVote(Guid userId, Guid messageId);
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

    public async Task<DiscussionMessage?> GetDiscussionMessageWithId(Guid dicussionMessageId)
    {
        return await _dbContext.DiscussionMessage.Where(m => m.Id == dicussionMessageId).FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateDiscussionMessageReview(DiscussionMessage discussionMessage)
    {
        _dbContext.DiscussionMessage.Update(discussionMessage);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<DiscussionMessageVote?> GetVoteByUserAndMessageId(Guid userId, Guid messageId)
    {
        return await _dbContext.DiscussionMessageVote
            .Where(vote => vote.UserId.Equals(userId) && vote.DiscussionMessageId.Equals(messageId))
            .FirstOrDefaultAsync();
    }

    public async Task<bool> InsertVote(DiscussionMessageVote vote)
    {
        using var transaction = _dbContext.Database.BeginTransaction();

        var existingVote = await GetVoteByUserAndMessageId(vote.UserId, vote.DiscussionMessageId);
        if (existingVote is not null)
        {
            await transaction.RollbackAsync();
            return true;
        }

        _dbContext.DiscussionMessageVote.Add(vote);
        var message = await _dbContext.DiscussionMessage
                .Where(message => message.Id.Equals(vote.DiscussionMessageId))
                .FirstOrDefaultAsync();

        if (message is null)
        {
            await transaction.RollbackAsync();
            return false;
        }

        message.VoteCount += vote.DiscussionMessageVoteValue == DiscussionMessageVoteValue.Upvote ? 1 : -1;
        var result = await _dbContext.SaveChangesAsync();
        await transaction.CommitAsync();
        return result > 0;
    }

    public async Task<bool> DeleteVote(Guid userId, Guid messageId)
    {
        using var transaction = _dbContext.Database.BeginTransaction();

        var vote = await GetVoteByUserAndMessageId(userId, messageId);
        if (vote is null)
        {
            await transaction.RollbackAsync();
            return true;
        }

        var message = await _dbContext.DiscussionMessage
                .Where(message => message.Id.Equals(vote.DiscussionMessageId))
                .FirstOrDefaultAsync();

        if (message is null)
        {
            await transaction.RollbackAsync();
            return false;
        }

        _dbContext.DiscussionMessageVote.Remove(vote);
        message.VoteCount -= vote.DiscussionMessageVoteValue == DiscussionMessageVoteValue.Upvote ? 1 : -1;
        var result = await _dbContext.SaveChangesAsync();
        await transaction.CommitAsync();
        return result > 0;
    }
}
