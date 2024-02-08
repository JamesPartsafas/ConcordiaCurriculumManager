using ConcordiaCurriculumManager.DTO.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.Security;

namespace ConcordiaCurriculumManager.Services;

public interface IDossierReviewService
{
    public Task SubmitDossierForReview(DossierSubmissionDTO dto);
    public Task RejectDossier(Guid dossierId, User user);
    public Task ReturnDossier(Guid dossierId, User user);
    public Task ForwardDossier(Guid dossierId, User user);
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
    private readonly ICourseRepository _courseRepository;

    public DossierReviewService(
        ILogger<DossierReviewService> logger,
        IDossierService dossierService,
        IGroupService groupService,
        ICourseService courseService,
        IDossierRepository dossierRepository,
        IDossierReviewRepository dossierReviewRepository,
        IUserAuthenticationService userAuthenticationService,
        IEmailService emailService,
        ICourseRepository courseRepository)
    {
        _logger = logger;
        _dossierService = dossierService;
        _groupService = groupService;
        _courseService = courseService;
        _dossierRepository = dossierRepository;
        _dossierReviewRepository = dossierReviewRepository;
        _userAuthenticationService = userAuthenticationService;
        _emailService = emailService;
        _courseRepository = courseRepository;
    }

    public async Task SubmitDossierForReview(DossierSubmissionDTO dto)
    {
        if (dto.GroupIds.Count == 0) throw new InvalidInputException("There must be at least one group set as a reviewer");
        if (!(await _groupService.IsGroupIdListValid(dto.GroupIds))) throw new InvalidInputException("All groups set as reviewers must be valid groups");

        Dossier dossier = await _dossierService.GetDossierDetailsByIdOrThrow(dto.DossierId);

        var approvalStages = dossier.PrepareForPublishing(dto);
        var isDossierSaved = await _dossierRepository.UpdateDossier(dossier);
        var areStagesSaved = await _dossierReviewRepository.SaveApprovalStages(approvalStages);
        dossier.ApprovalStages = approvalStages;

        if (isDossierSaved && areStagesSaved)
        {
            _ = SendEmailToCurrentApprovingGroup(dossier, "Pending Dossier Review", EmailTemplates.GetDossierPendingReviewTemplate);
            _logger.LogInformation($"Dossier {dossier.Id} submitted for review to group {approvalStages.First().Id}");
        }
        else
        {
            _logger.LogError($"Encountered error attempting to submit dossier {dossier.Id} for review");
        }
    }

    public async Task RejectDossier(Guid dossierId, User user)
    {
        Dossier dossier = await GetDossierWithApprovalStagesOrThrow(dossierId);

        dossier.MarkAsRejected(user);

        var isDossierSaved = await _dossierRepository.UpdateDossier(dossier);
        var emailAddress = dossier.Initiator?.Email;

        if (emailAddress is null)
        {
            var initiator = await _dossierRepository.GetDossierInitiator(dossier.Id);
            emailAddress = initiator.Email;
        }

        if (isDossierSaved)
        {
            _ = _emailService.SendEmail(emailAddress, $"Dossier Status Change", EmailTemplates.GetDossierRejectedTemplate(dossier.Title, dossier.Id));
            _logger.LogInformation($"Dossier {dossier.Id} successfully rejected from the review process");
        }
        else
        {
            _logger.LogError($"Encountered error attempting to reject dossier {dossier.Id} from the review process");
        }
    }

    public async Task ReturnDossier(Guid dossierId, User user)
    {
        Dossier dossier = await GetDossierWithApprovalStagesOrThrow(dossierId);

        dossier.MarkAsReturned(user);

        var isDossierSaved = await _dossierRepository.UpdateDossier(dossier);
        var emailAddress = dossier.Initiator?.Email;

        if (emailAddress is null)
        {
            var initiator = await _dossierRepository.GetDossierInitiator(dossier.Id);
            emailAddress = initiator.Email;
        }

        if (isDossierSaved)
        {
            _ = _emailService.SendEmail(emailAddress, $"Dossier Status Change", EmailTemplates.GetDossierReturnedTemplate(dossier.Title, dossier.Id));
            _ = SendEmailToCurrentApprovingGroup(dossier, "Pending Dossier Review", EmailTemplates.GetDossierPendingReviewTemplate);
            _logger.LogInformation($"Dossier {dossier.Id} successfully returned to the previous group in the review process");
        }
        else
        {
            _logger.LogError($"Encountered error attempting to return dossier {dossier.Id} to the previous group in the review process");
        }
    }

    public async Task ForwardDossier(Guid dossierId, User user)
    {
        Dossier dossier = await GetDossierWithApprovalStagesAndRequestsOrThrow(dossierId);

        if (dossier.IsInFinalStageOfReviewPipeline())
            await AcceptDossierChanges(dossier, user);
        else
            await ForwardDossierToNextGroup(dossier, user);
    }

    private async Task ForwardDossierToNextGroup(Dossier dossier, User user)
    {
        dossier.MarkAsForwarded(user);

        var isDossierSaved = await _dossierRepository.UpdateDossier(dossier);
        var emailAddress = dossier.Initiator?.Email;

        if (emailAddress is null)
        {
            var initiator = await _dossierRepository.GetDossierInitiator(dossier.Id);
            emailAddress = initiator.Email;
        }

        if (isDossierSaved)
        {
            _ = _emailService.SendEmail(emailAddress, $"Dossier Status Change", EmailTemplates.GetDossierForwardedTemplate(dossier.Title, dossier.Id));
            _ = SendEmailToCurrentApprovingGroup(dossier, "Pending Dossier Review", EmailTemplates.GetDossierPendingReviewTemplate);
            _logger.LogInformation($"Dossier {dossier.Id} successfully forwarded to the next group in the review process");
        }
        else
        {
            _logger.LogError($"Encountered error attempting to forward dossier {dossier.Id} to the next group in the review process");
        }
    }

    private async Task AcceptDossierChanges(Dossier dossier, User user)
    {
        var courseVersions = await _courseService.GetCourseVersions(dossier);

        dossier.MarkAsAccepted(courseVersions, user);

        var isDossierSaved = await _dossierRepository.UpdateDossier(dossier);
        var emailAddress = dossier.Initiator?.Email;

        if (emailAddress is null)
        {
            var initiator = await _dossierRepository.GetDossierInitiator(dossier.Id);
            emailAddress = initiator.Email;
        }

        if (isDossierSaved)
        {
            _ = _emailService.SendEmail(emailAddress, $"Dossier Status Change", EmailTemplates.GetDossierApprovalTemplate(dossier.Title, dossier.Id)); ;
            _logger.LogInformation($"Dossier {dossier.Id} successfully accepted and removed from the review process");
        }
        else
        {
            _logger.LogError($"Encountered error attempting to accept dossier {dossier.Id} and remove it from the review process");
        }

        var dossiers = await _dossierRepository.GetAllNonApprovedDossiers();

        foreach (var request in dossier.CourseCreationRequests)
        {
            await ChangeAllCourseRequests(dossiers, request.NewCourse!.Subject, request.NewCourse.Catalog, "creation");
        }

        foreach (var request in dossier.CourseDeletionRequests)
        {
            await ChangeAllCourseRequests(dossiers, request.Course!.Subject, request.Course.Catalog, "deletion");
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

    private async Task ChangeAllCourseRequests(IList<Dossier> dossiers, string subject, string catalog, string type)
    {
        foreach (var d in dossiers)
        {
            if (type == "creation")
            {
                foreach (var ccr in d.CourseCreationRequests)
                {
                    var course = ccr.NewCourse;
                    if (course!.Subject == subject && course.Catalog == catalog)
                    {
                        var clonedCourse = Course.CloneCourse(course);
                        var courseModificationRequest = new CourseModificationRequest
                        {
                            Id = Guid.NewGuid(),
                            CourseId = clonedCourse.Id,
                            Course = clonedCourse,
                            DossierId = d.Id,
                            Rationale = ccr.Rationale,
                            ResourceImplication = ccr.ResourceImplication,
                            Comment = ccr.Comment,
                            Conflict = ccr.Conflict
                        };
                        d.CourseCreationRequests = d.CourseCreationRequests.Where(request => request.NewCourse!.CourseID != course.CourseID).ToList();
                        await _dossierRepository.DeleteCourseCreationRequest(ccr);
                        await _courseRepository.SaveCourse(clonedCourse);
                        await _dossierRepository.SaveCourseModificationRequest(courseModificationRequest);
                    }

                }
            }
            if (type == "deletion")
            {
                foreach (var cmr in d.CourseModificationRequests)
                {
                    var course = cmr.Course;
                    if (course!.Subject == subject && course.Catalog == catalog)
                    {
                        var clonedCourse = Course.CloneCourse(course);
                        var courseCreationRequest = new CourseCreationRequest
                        {
                            Id = Guid.NewGuid(),
                            NewCourseId = clonedCourse.Id,
                            NewCourse = clonedCourse,
                            DossierId = d.Id,
                            Rationale = cmr.Rationale,
                            ResourceImplication = cmr.ResourceImplication,
                            Comment = cmr.Comment,
                            Conflict = cmr.Conflict,
                        };
                        d.CourseModificationRequests = d.CourseModificationRequests.Where(request => request.Course!.CourseID != course.CourseID).ToList();
                        await _dossierRepository.DeleteCourseModificationRequest(cmr);
                        await _courseRepository.SaveCourse(clonedCourse);
                        await _dossierRepository.SaveCourseCreationRequest(courseCreationRequest);
                    }
                }
                foreach (var cdr in d.CourseDeletionRequests)
                {
                    var course = cdr.Course;
                    if (course!.Subject == subject && course.Catalog == catalog)
                    {
                        d.CourseDeletionRequests = d.CourseDeletionRequests.Where(request => request.Course!.CourseID != course.CourseID).ToList();
                        await _dossierRepository.DeleteCourseDeletionRequest(cdr);
                    }
                }
            }
        }
    }

    private async Task SendEmailToCurrentApprovingGroup(Dossier dossier, string subject, Func<string, Guid, string> EmailTemplateFunc)
    {
        var currentApprovingGroup = dossier.ApprovalStages.Where(stage => stage.IsCurrentStage).FirstOrDefault() ?? throw new BadRequestException("Unexpected error occurred: Could not find current group");
        var groupEmails = await _groupService.GetAllGroupMembersAndMastersEmails(currentApprovingGroup.GroupId);
        await Task.WhenAll(groupEmails.Select(async (email) => await _emailService.SendEmail(email, subject, EmailTemplateFunc(dossier.Title, dossier.Id))));
    }
}
