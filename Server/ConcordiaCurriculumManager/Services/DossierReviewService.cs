using ConcordiaCurriculumManager.DTO.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Repositories;

namespace ConcordiaCurriculumManager.Services;

public interface IDossierReviewService
{
    public Task SubmitDossierForReview(DossierSubmissionDTO dto);
}

public class DossierReviewService : IDossierReviewService
{
    private readonly ILogger<DossierReviewService> _logger;
    private readonly IDossierService _dossierService;
    private readonly IGroupService _groupService;
    private readonly IDossierRepository _dossierRepository;
    private readonly IDossierReviewRepository _dossierReviewRepository;

    public DossierReviewService(
        ILogger<DossierReviewService> logger,
        IDossierService dossierService,
        IGroupService groupService,
        IDossierRepository dossierRepository,
        IDossierReviewRepository dossierReviewRepository)
    {
        _logger = logger;
        _dossierService = dossierService;
        _groupService = groupService;
        _dossierRepository = dossierRepository;
        _dossierReviewRepository = dossierReviewRepository;
    }

    public async Task SubmitDossierForReview(DossierSubmissionDTO dto)
    {
        if (dto.GroupIds.Count == 0) throw new InvalidInputException("There must be at least one group set as a reviewer");
        if (!(await _groupService.IsGroupIdListValid(dto.GroupIds))) throw new InvalidInputException("All groups set as reviewers must be valid groups");

        Dossier dossier = await _dossierService.GetDossierDetailsByIdOrThrow(dto.DossierId);
        if (dossier.Published) throw new BadRequestException("The dossier has already been submitted for review");

        var approvalStages = dossier.PrepareForPublishing(dto);
        var isDossierSaved = await _dossierRepository.UpdateDossier(dossier);
        var areStagesSaved = await _dossierReviewRepository.SaveApprovalStages(approvalStages);

        if (isDossierSaved && areStagesSaved)
            _logger.LogInformation($"Dossier {dossier.Id} submitted for review to group {approvalStages.First().Id}");
        else
            _logger.LogError($"Encountered error attempting to submit dossier {dossier.Id} for review");
    }
}
