using AutoMapper;
using ConcordiaCurriculumManager.DTO.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Security;
using ConcordiaCurriculumManager.Security.Requirements;
using ConcordiaCurriculumManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ConcordiaCurriculumManager.SignalR;

[Authorize]
public class DossierDiscussionSignalR : Hub
{
    private readonly IMapper _mapper;
    private readonly IDossierService _dossierService;
    private readonly IDossierReviewService _dossierReviewService;

    public DossierDiscussionSignalR(IMapper mapper, IDossierReviewService dossierReviewService, IDossierService dossierService)
    {
        _mapper = mapper;
        _dossierReviewService = dossierReviewService;
        _dossierService = dossierService;
    }

    public async Task ReviewDossier(Guid dossierId, CreateDossierDiscussionMessageDTO dossierMessageDTO)
    {
        var userId = GetCurrentUserClaim(Claims.Id);
        var validUserId = Guid.TryParse(userId, out var parsedUserId);

        if (userId is null || !validUserId)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("Error", "Cannot post a review due to expired token");
            return;
        }

        var httpContext = Context.GetHttpContext();
        if (httpContext is null)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("Error", "Could not access the query");
            return;
        }

        httpContext.Request.Query.TryGetValue("dossierId", out var queryDossierId);
        var validDossierId = Guid.TryParse(queryDossierId.ToString(), out var parsedDossierId);

        if (!validDossierId || !parsedDossierId.Equals(dossierId))
        {
            await Clients.Client(Context.ConnectionId).SendAsync("Error", "Client error. dossier ids do not match");
            return;
        }

        var isOwner = await IsOwnerOfPublishedDossier(parsedUserId, parsedDossierId);

        if (!isOwner)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("Error", "Client error. Cannot review a dossier when the dossier is not published yet and/or you are not an owner of the dossier");
            return;
        }

        var dossierMessage = _mapper.Map<DiscussionMessage>(dossierMessageDTO);

        if (dossierMessage is null)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("Error", "Invalid message");
            return;
        }

        dossierMessage.DossierDiscussionId = parsedDossierId;
        await _dossierReviewService.AddDossierDiscussionReview(parsedDossierId, dossierMessage, parsedUserId);

        var outputMessage = _mapper.Map<DossierDiscussionMessageDTO>(dossierMessage);

        if (outputMessage is null)
        {
            await Clients.All.SendAsync("Error", "Server error could not send the message. Please try to refresh the page.");
            return;
        }

        await Clients.All.SendAsync(nameof(ReviewDossier), parsedDossierId.ToString(), outputMessage);
    }

    private string? GetCurrentUserClaim(string claim)
    {
        if (Context.User is null)
        {
            return null;
        }

        return Context.User.Claims.FirstOrDefault(i => i.Type.ToString() == claim)?.Value;
    }

    private async Task<bool> IsOwnerOfPublishedDossier(Guid userId, Guid dossierId)
    {
        var dossier = await _dossierService.GetDossierDetailsById(dossierId);
        if (dossier is null)
        {
            return false;
        }

        var isDossierPublished = !dossier.State.Equals(DossierStateEnum.Created);

        if (!isDossierPublished)
        {
            return false;
        }

        var currentApprovalStage = dossier.ApprovalStages.Where(stage => stage.IsCurrentStage).FirstOrDefault();
        var reviewingGroup = currentApprovalStage?.Group;

        if (reviewingGroup is null)
        {
            return false;
        }

        if (isDossierPublished && !reviewingGroup.Members.Exists(m => m.Id.Equals(userId)))
        {
            return false;
        }

        return true;
    }
}
