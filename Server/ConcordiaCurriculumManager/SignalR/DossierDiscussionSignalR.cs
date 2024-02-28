using AutoMapper;
using ConcordiaCurriculumManager.DTO.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Security;
using ConcordiaCurriculumManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ConcordiaCurriculumManager.SignalR;

[Authorize]
public class DossierDiscussionSignalR : Hub
{
    private readonly IMapper _mapper;
    private readonly IDossierReviewService _dossierReviewService;

    public DossierDiscussionSignalR(IMapper mapper, IDossierReviewService dossierReviewService)
    {
        _mapper = mapper;
        _dossierReviewService = dossierReviewService;
    }

    [Authorize(Policies.IsOwnerOfDossier)]
    public async Task ReviewDossier(Guid dossierId, CreateDossierDiscussionMessageDTO dossierMessageDTO)
    {
        var userId = GetCurrentUserClaim(Claims.Id);
        var validUserId = Guid.TryParse(userId, out var parsedUserId);

        if (userId is null || !validUserId)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("Error", "Cannot post a review due to expired token");
            return;
        }

        var dossierMessage = _mapper.Map<DiscussionMessage>(dossierMessageDTO);

        if (dossierMessage is null)
        {
            await Clients.Client(Context.ConnectionId).SendAsync("Error", "Invalid message");
            return;
        }

        Context.Items.TryGetValue("dossierId", out var authDossierId);
        var validDossierId = Guid.TryParse(authDossierId?.ToString(), out var parsedDossierId);

        if (!validDossierId || !parsedDossierId.Equals(dossierId))
        {
            await Clients.Client(Context.ConnectionId).SendAsync("Error", "Client error. dossier ids do not match");
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
}
