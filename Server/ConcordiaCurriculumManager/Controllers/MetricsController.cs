using AutoMapper;
using ConcordiaCurriculumManager.DTO.Metrics;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ConcordiaCurriculumManager.Controllers;

[ApiController]
[Route("[controller]")]
//[Authorize(Roles = RoleNames.Admin)]
public class MetricsController : Controller
{
    private readonly IMapper _mapper;
    private readonly IMetricsService _metricsService;

    public MetricsController(IMapper mapper, IMetricsService metricsService)
    {
        _mapper = mapper;
        _metricsService = metricsService;
    }

    [HttpGet(nameof(GetTopHttpStatusCodes))]
    [SwaggerResponse(StatusCodes.Status200OK, "Current top list of http status codes was retrieved")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User is not authorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    public async Task<ActionResult> GetTopHttpStatusCodes([FromQuery] DateOnly? startDate, [FromQuery] int fromIndex = 0)
    {
        if (fromIndex < 0)
        {
            return BadRequest("From Index cannot be less than 0");
        }

        startDate ??= DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-5));
        var httpStatusCodes = await _metricsService.GetTopHttpStatusCodes(fromIndex, (DateOnly)startDate);
        var returnResult = _mapper.Map<HttpStatusCountWrapperDTO>(httpStatusCodes);
        returnResult.NextIndex += fromIndex;

        return Ok(returnResult);
    }

    [HttpGet(nameof(GetTopHitHttpEndpoints))]
    [SwaggerResponse(StatusCodes.Status200OK, "Current top list of http endpoints was retrieved")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User is not authorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    public async Task<ActionResult> GetTopHitHttpEndpoints([FromQuery] DateOnly? startDate, [FromQuery] int fromIndex = 0)
    {
        if (fromIndex < 0)
        {
            return BadRequest("From Index cannot be less than 0");
        }

        startDate ??= DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-5));
        var httpStatusCodes = await _metricsService.GetTopHitHttpEndpoints(fromIndex, (DateOnly)startDate);
        var returnResult = _mapper.Map<HttpEndpointCountWrapperDTO>(httpStatusCodes);
        returnResult.NextIndex += fromIndex;

        return Ok(returnResult);
    }

    [HttpGet(nameof(GetTopViewedDossier))]
    [SwaggerResponse(StatusCodes.Status200OK, "Current top list of http endpoints was retrieved")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User is not authorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    public async Task<ActionResult> GetTopViewedDossier([FromQuery] DateOnly? startDate, [FromQuery] int fromIndex = 0)
    {
        if (fromIndex < 0)
        {
            return BadRequest("From Index cannot be less than 0");
        }

        startDate ??= DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-5));
        var httpStatusCodes = await _metricsService.GetTopViewedDossier(fromIndex, (DateOnly)startDate);
        var returnResult = _mapper.Map<DossierViewCountWrapperDTO>(httpStatusCodes);
        returnResult.NextIndex += fromIndex;

        return Ok(returnResult);
    }


    [HttpGet(nameof(GetTopDossierViewingUser))]
    [SwaggerResponse(StatusCodes.Status200OK, "Current top list of http endpoints was retrieved")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User is not authorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    public async Task<ActionResult> GetTopDossierViewingUser([FromQuery] DateOnly? startDate, [FromQuery] int fromIndex = 0)
    {
        if (fromIndex < 0)
        {
            return BadRequest("From Index cannot be less than 0");
        }

        startDate ??= DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-5));
        var httpStatusCodes = await _metricsService.GetTopDossierViewingUser(fromIndex, (DateOnly)startDate);
        var returnResult = _mapper.Map<UserDossierViewedCountWrapperDTO>(httpStatusCodes);
        returnResult.NextIndex += fromIndex;

        return Ok(returnResult);
    }
}
