using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace ConcordiaCurriculumManager.Security.Requirements.Handlers;

public class DossierReviewMasterHandler : AuthorizationHandler<DossierReviewMasterRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<GroupMasterHandler> _logger;
    private readonly IDossierService _dossierService;

    public DossierReviewMasterHandler(
        ILogger<GroupMasterHandler> logger,
        IDossierService dossierService,
        IHttpContextAccessor contextAccessor)
    {
        _logger = logger;
        _dossierService = dossierService;
        _httpContextAccessor = contextAccessor;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, DossierReviewMasterRequirement requirement)
    {
        _logger.LogInformation("Evaluting DossierReviewMasterHandler");

        if (_httpContextAccessor.HttpContext is null)
        {
            // This is not an Http Request or there is no group Id. Abstain
            _logger.LogWarning("Context is null");
            return;
        }

        if (context.User.Identity is null || !context.User.Identity.IsAuthenticated)
        {
            context.Fail();
            return;
        }

        if (_httpContextAccessor.HttpContext is null
            || !_httpContextAccessor.HttpContext.Request.RouteValues.TryGetValue("dossierId", out var dossierId)
            || !Guid.TryParse(dossierId?.ToString(), out var parsedDossierId))
        {
            // This is not an Http Request or there is no dossier Id. Abstain
            _logger.LogWarning("DossierReviewMasterHandler is possibly called on a http endpoint that does not include a dossier id as a param");
            return;
        }

        var userId = context.User.Claims.FirstOrDefault(c => c.Type.Equals(Claims.Id));

        if (userId is null || !Guid.TryParse(userId.Value, out var parsedUserId))
        {
            _logger.LogWarning("User is authenticated without a valid Id claim");
            context.Fail();
            return;
        }

        IList<User> groupMasters = await _dossierService.GetCurrentlyReviewingGroupMasters(parsedDossierId);

        // Check if requesting user is not a master of the current reviewing group
        if (groupMasters.Where(master => master.Id.Equals(parsedUserId)).IsNullOrEmpty())
        {
            context.Fail();
            return;
        }

        context.Succeed(requirement);
    }
}
