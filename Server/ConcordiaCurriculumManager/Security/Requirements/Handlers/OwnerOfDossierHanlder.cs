using ConcordiaCurriculumManager.Services;
using Microsoft.AspNetCore.Authorization;

namespace ConcordiaCurriculumManager.Security.Requirements.Handlers;

public class OwnerOfDossierHandler : AuthorizationHandler<OwnerOfDossierRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<OwnerOfDossierHandler> _logger;
    private readonly IDossierService _dossierService;

    public OwnerOfDossierHandler(IHttpContextAccessor httpContextAccessor, ILogger<OwnerOfDossierHandler> logger, IDossierService dossierService)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _dossierService = dossierService;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnerOfDossierRequirement requirement)
    {
        _logger.LogInformation("Evaluting OwnerOfDossierHanlder");

        if (_httpContextAccessor.HttpContext is null
            || !_httpContextAccessor.HttpContext.Request.RouteValues.TryGetValue("dossierId", out var dossierId)
            || !Guid.TryParse(dossierId?.ToString(), out var parsedDossierId))
        {
            // This is not an Http Request or there is no group Id. Abstain
            _logger.LogWarning("OwnerOfDossierHandler is possibly called on a http endpoint that does not include a dossier id as a param");
            return;
        }

        if (context.User.Identity is null || !context.User.Identity.IsAuthenticated)
        {
            context.Fail();
            return;
        }

        var userId = context.User.Claims.FirstOrDefault(c => c.Type.Equals(Claims.Id));

        if (userId is null || !Guid.TryParse(userId.Value, out var parsedUserId))
        {
            _logger.LogWarning("User is authenticated without a valid Id claim");
            context.Fail();
            return;
        }

        var dossier = await _dossierService.GetDossierDetailsById(parsedDossierId);

        if (dossier is null || !dossier.InitiatorId.Equals(parsedUserId))
        {
            context.Fail();
            return;
        }

        context.Succeed(requirement);
    }
}
