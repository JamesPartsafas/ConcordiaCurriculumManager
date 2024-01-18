using ConcordiaCurriculumManager.DTO.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.Security;

namespace ConcordiaCurriculumManager.Services;

public interface IDossierReviewService
{
    public Task SubmitDossierForReview(DossierSubmissionDTO dto);
    public Task RejectDossier(Guid dossierId);
    public Task ReturnDossier(Guid dossierId);
    public Task ForwardDossier(Guid dossierId);
    public Task<Dossier> GetDossierWithApprovalStagesOrThrow(Guid dossierId);
    public Task<Dossier> GetDossierWithApprovalStagesAndRequestsOrThrow(Guid dossierId);
    public Task<Dossier> GetDossierWithApprovalStagesAndRequestsAndDiscussionOrThrow(Guid dossierId);
    public Task AddDossierDiscussionReview(Guid dossierId, DiscussionMessage message);
}

public class DossierReviewService : IDossierReviewService
{
    private readonly ILogger<DossierReviewService> _logger;
    private readonly IDossierService _dossierService;
    private readonly IGroupService _groupService;
    private readonly ICourseService _courseService;
    private readonly IDossierRepository _dossierRepository;
    private readonly IDossierReviewRepository _dossierReviewRepository;
    private readonly IUserAuthenticationService _userAuthenticationService;
    private readonly IEmailService _emailService;

    public DossierReviewService(
        ILogger<DossierReviewService> logger,
        IDossierService dossierService,
        IGroupService groupService,
        ICourseService courseService,
        IDossierRepository dossierRepository,
        IDossierReviewRepository dossierReviewRepository,
        IUserAuthenticationService userAuthenticationService,
        IEmailService emailService)
    {
        _logger = logger;
        _dossierService = dossierService;
        _groupService = groupService;
        _courseService = courseService;
        _dossierRepository = dossierRepository;
        _dossierReviewRepository = dossierReviewRepository;
        _userAuthenticationService = userAuthenticationService;
        _emailService = emailService;
    }

    public async Task SubmitDossierForReview(DossierSubmissionDTO dto)
    {
        if (dto.GroupIds.Count == 0) throw new InvalidInputException("There must be at least one group set as a reviewer");
        if (!(await _groupService.IsGroupIdListValid(dto.GroupIds))) throw new InvalidInputException("All groups set as reviewers must be valid groups");

        Dossier dossier = await _dossierService.GetDossierDetailsByIdOrThrow(dto.DossierId);

        var approvalStages = dossier.PrepareForPublishing(dto);
        var isDossierSaved = await _dossierRepository.UpdateDossier(dossier);
        var areStagesSaved = await _dossierReviewRepository.SaveApprovalStages(approvalStages);

        if (isDossierSaved && areStagesSaved)
            _logger.LogInformation($"Dossier {dossier.Id} submitted for review to group {approvalStages.First().Id}");
        else
            _logger.LogError($"Encountered error attempting to submit dossier {dossier.Id} for review");
    }

    public async Task RejectDossier(Guid dossierId)
    {
        Dossier dossier = await GetDossierWithApprovalStagesOrThrow(dossierId);

        dossier.MarkAsRejected();

        var isDossierSaved = await _dossierRepository.UpdateDossier(dossier);
        if (isDossierSaved)
            _logger.LogInformation($"Dossier {dossier.Id} successfully rejected from the review process");
        else
            _logger.LogError($"Encountered error attempting to reject dossier {dossier.Id} from the review process");
    }

    public async Task ReturnDossier(Guid dossierId)
    {
        Dossier dossier = await GetDossierWithApprovalStagesOrThrow(dossierId);

        dossier.MarkAsReturned();

        var isDossierSaved = await _dossierRepository.UpdateDossier(dossier);
        if (isDossierSaved)
            _logger.LogInformation($"Dossier {dossier.Id} successfully returned to the previous group in the review process");
        else
            _logger.LogError($"Encountered error attempting to return dossier {dossier.Id} to the previous group in the review process");
    }

    public async Task ForwardDossier(Guid dossierId)
    {
        Dossier dossier = await GetDossierWithApprovalStagesAndRequestsOrThrow(dossierId);

        if (dossier.IsInFinalStageOfReviewPipeline())
            await AcceptDossierChanges(dossier);
        else
            await ForwardDossierToNextGroup(dossier);
    }

    private async Task ForwardDossierToNextGroup(Dossier dossier)
    {
        dossier.MarkAsForwarded();

        var isDossierSaved = await _dossierRepository.UpdateDossier(dossier);
        if (isDossierSaved)
            _logger.LogInformation($"Dossier {dossier.Id} successfully forwarded to the next group in the review process");
        else
            _logger.LogError($"Encountered error attempting to forward dossier {dossier.Id} to the next group in the review process");
    }

    private async Task AcceptDossierChanges(Dossier dossier)
    {
        var courseVersions = await _courseService.GetCourseVersions(dossier);

        dossier.MarkAsAccepted(courseVersions);

        var isDossierSaved = await _dossierRepository.UpdateDossier(dossier);
        var emailAddress = dossier.Initiator?.Email;

        if (emailAddress is null)
        {
            var initiator = await _dossierRepository.GetDossierInitiator(dossier.Id);
            emailAddress = initiator.Email;
        }

        if (isDossierSaved)
        {
            _ = _emailService.SendEmail(emailAddress, $"Dossier {dossier.Title} has been approved", $"{dossier.Title} ({dossier.Id}) has been approved!");
            _logger.LogInformation($"Dossier {dossier.Id} successfully accepted and removed from the review process");
        }
        else
        {
            _logger.LogError($"Encountered error attempting to accept dossier {dossier.Id} and remove it from the review process");
        }
    }

    public async Task AddDossierDiscussionReview(Guid dossierId, DiscussionMessage message)
    {
        var userId = _userAuthenticationService.GetCurrentUserClaim(Claims.Id);
        var dossier = await GetDossierWithApprovalStagesAndRequestsAndDiscussionOrThrow(dossierId);

        if (dossier.State.Equals(DossierStateEnum.Created))
        {
            throw new BadRequestException("The dossier is not published yet.");
        }

        message.AuthorId = Guid.Parse(userId);
        dossier.Discussion.Messages.Add(message);

        var isDossierSaved = await _dossierRepository.UpdateDossier(dossier);
        if (isDossierSaved)
            _logger.LogInformation($"Discussion message was successfully saved to dossier {dossier.Id}");
        else
            _logger.LogError($"Encountered error attempting to save a discussion message to dossier {dossier.Id}");
    }

    public async Task<Dossier> GetDossierWithApprovalStagesOrThrow(Guid dossierId) => await _dossierReviewRepository.GetDossierWithApprovalStages(dossierId)
        ?? throw new NotFoundException("The dossier does not exist.");

    public async Task<Dossier> GetDossierWithApprovalStagesAndRequestsOrThrow(Guid dossierId) =>
        await _dossierReviewRepository.GetDossierWithApprovalStagesAndRequests(dossierId)
        ?? throw new NotFoundException("The dossier does not exist.");

    public async Task<Dossier> GetDossierWithApprovalStagesAndRequestsAndDiscussionOrThrow(Guid dossierId) =>
        await _dossierRepository.GetDossierByDossierId(dossierId)
        ?? throw new NotFoundException("The dossier does not exist.");
}
