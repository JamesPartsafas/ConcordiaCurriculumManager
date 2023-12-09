using ConcordiaCurriculumManager.DTO.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Repositories;

namespace ConcordiaCurriculumManager.Services;

public interface IDossierReviewService
{
    public Task SubmitDossierForReview(DossierSubmissionDTO dto);
    public Task RejectDossier(Guid dossierId);
    public Task ForwardDossier(Guid dossierId);
    public Task<Dossier> GetDossierWithApprovalStagesOrThrow(Guid dossierId);
    public Task<Dossier> GetDossierWithApprovalStagesAndRequestsOrThrow(Guid dossierId);
}

public class DossierReviewService : IDossierReviewService
{
    private readonly ILogger<DossierReviewService> _logger;
    private readonly IDossierService _dossierService;
    private readonly IGroupService _groupService;
    private readonly ICourseService _courseService;
    private readonly IDossierRepository _dossierRepository;
    private readonly IDossierReviewRepository _dossierReviewRepository;

    public DossierReviewService(
        ILogger<DossierReviewService> logger,
        IDossierService dossierService,
        IGroupService groupService,
        ICourseService courseService,
        IDossierRepository dossierRepository,
        IDossierReviewRepository dossierReviewRepository)
    {
        _logger = logger;
        _dossierService = dossierService;
        _groupService = groupService;
        _courseService = courseService;
        _dossierRepository = dossierRepository;
        _dossierReviewRepository = dossierReviewRepository;
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
        if (isDossierSaved)
            _logger.LogInformation($"Dossier {dossier.Id} successfully accepted and removed from the review process");
        else
            _logger.LogError($"Encountered error attempting to accept dossier {dossier.Id} and remove it from the review process");
    }

    public async Task<Dossier> GetDossierWithApprovalStagesOrThrow(Guid dossierId) => await _dossierReviewRepository.GetDossierWithApprovalStages(dossierId)
        ?? throw new NotFoundException("The dossier does not exist.");

    public async Task<Dossier> GetDossierWithApprovalStagesAndRequestsOrThrow(Guid dossierId) => 
        await _dossierReviewRepository.GetDossierWithApprovalStagesAndRequests(dossierId)
        ?? throw new NotFoundException("The dossier does not exist.");
}
