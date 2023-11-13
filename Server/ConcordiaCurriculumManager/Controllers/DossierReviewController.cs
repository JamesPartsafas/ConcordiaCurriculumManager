using ConcordiaCurriculumManager.DTO.Dossiers.DossierReview;
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
    public async Task<ActionResult> SubmitDossierForReview([FromBody, Required] DossierSubmissionDTO dto)
    {
        await _dossierReviewService.SubmitDossierForReview(dto);

        return Ok();
    }
}
