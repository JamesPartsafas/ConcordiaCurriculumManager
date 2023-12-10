using ConcordiaCurriculumManager.DTO.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Security;
using ConcordiaCurriculumManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace ConcordiaCurriculumManager.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class DossierReviewController : Controller
{
    private readonly IDossierReviewService _dossierReviewService;

    public DossierReviewController(IDossierReviewService dossierReviewService)
    {
        _dossierReviewService = dossierReviewService;
    }

    [HttpPost(nameof(SubmitDossierForReview) + "/{dossierId}")]
    [Authorize(Policies.IsOwnerOfDossier)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status200OK, "Dossier successfully submitted for review")]
    public async Task<ActionResult> SubmitDossierForReview([FromBody, Required] DossierSubmissionDTO dto, [FromRoute, Required] Guid dossierId)
    {
        if (dossierId != dto.DossierId)
            throw new InvalidInputException("The provided dossierId in the route must match the one in the DTO");

        await _dossierReviewService.SubmitDossierForReview(dto);

        return NoContent();
    }

    [HttpPut(nameof(RejectDossier) + "/{dossierId}")]
    [Authorize(Policies.IsDossierReviewMaster)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status200OK, "Dossier successfully rejected")]
    public async Task<ActionResult> RejectDossier([FromRoute, Required] Guid dossierId)
    {
        await _dossierReviewService.RejectDossier(dossierId);

        return NoContent();
    }

    [HttpPut(nameof(ForwardDossier) + "/{dossierId}")]
    [Authorize(Policies.IsDossierReviewMaster)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status200OK, "Dossier successfully forwarded")]
    public async Task<ActionResult> ForwardDossier([FromRoute, Required] Guid dossierId)
    {
        await _dossierReviewService.ForwardDossier(dossierId);

        return NoContent();
    }
}
