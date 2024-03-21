using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Metrics;
using ConcordiaCurriculumManager.Security;
using ConcordiaCurriculumManager.Services;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ConcordiaCurriculumManager.Middleware.Metrics;

public class AddDossierMetric : IAsyncActionFilter
{
    private readonly ILogger<AddDossierMetric> _logger;
    private readonly IMetricsService _metricsService;
    private readonly IDossierService _dossierService;

    public AddDossierMetric(ILogger<AddDossierMetric> logger, IMetricsService metricsService, IDossierService dossierService)
    {
        _logger = logger;
        _metricsService = metricsService;
        _dossierService = dossierService;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var httpContext = context.HttpContext;

        if (httpContext.User.Identity is null || !httpContext.User.Identity.IsAuthenticated)
        {
            _logger.LogWarning("AddDossierMetric was called when the user is not authenticated.");
            await next();
            return;
        }

        var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type.Equals(Claims.Id));
        if (userId is null || !Guid.TryParse(userId.Value, out var parsedUserId))
        {
            _logger.LogError("AddDossierMetric was called when on an authenticated user with an invalid userId.");
            await next();
            return;
        }

        if (!httpContext.Request.RouteValues.TryGetValue("dossierId", out var dossierId)
            || !Guid.TryParse(dossierId?.ToString(), out var parsedDossierId))
        {
            _logger.LogWarning("AddDossierMetric was called without a valid dossierId.");
            await next();
            return;
        }

        var dossier = await _dossierService.GetDossierDetailsById(parsedDossierId);

        if (dossier is null)
        {
            _logger.LogWarning("AddDossierMetric was called with a dossierId that does not exist.");
            await next();
            return;
        }

        var isDossierPublished = !dossier.State.Equals(DossierStateEnum.Created);

        if (!isDossierPublished)
        {
            _logger.LogInformation("AddDossierMetric was called on a non-published dossier.");
            await next();
            return;
        }

        var dossierMetric = new DossierMetric {
            DossierId = parsedDossierId,
            UserId = parsedUserId
        };

        await _metricsService.SaveDossierMetric(dossierMetric);
        await next();
    }
}
