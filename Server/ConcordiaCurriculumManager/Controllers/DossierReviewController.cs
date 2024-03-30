using AutoMapper;
using ConcordiaCurriculumManager.DTO.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Models.Users;
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
    private readonly IMapper _mapper;
    private readonly IDossierReviewService _dossierReviewService;
    private readonly IUserAuthenticationService _userAuthenticationService;

    public DossierReviewController(IDossierReviewService dossierReviewService, IMapper mapper, IUserAuthenticationService userAuthenticationService)
    {
        _dossierReviewService = dossierReviewService;
        _mapper = mapper;
        _userAuthenticationService = userAuthenticationService;
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
        User user = await _userAuthenticationService.GetCurrentUser();
        await _dossierReviewService.RejectDossier(dossierId, user);

        return NoContent();
    }

    [HttpPut(nameof(ReturnDossier) + "/{dossierId}")]
    [Authorize(Policies.IsDossierReviewMaster)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status200OK, "Dossier successfully returned")]
    public async Task<ActionResult> ReturnDossier([FromRoute, Required] Guid dossierId)
    {
        User user = await _userAuthenticationService.GetCurrentUser();
        await _dossierReviewService.ReturnDossier(dossierId, user);

        return NoContent();
    }

    [HttpPut(nameof(ForwardDossier) + "/{dossierId}")]
    [Authorize(Policies.IsDossierReviewMaster)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status200OK, "Dossier successfully forwarded")]
    public async Task<ActionResult> ForwardDossier([FromRoute, Required] Guid dossierId)
    {
        User user = await _userAuthenticationService.GetCurrentUser();
        await _dossierReviewService.ForwardDossier(dossierId, user);

        return NoContent();
    }

    [HttpPost(nameof(ReviewDossier) + "/{dossierId}")]
    [Authorize(Policies.IsOwnerOfDossier)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status200OK, "Dossier Review successfully added")]
    public async Task<ActionResult> ReviewDossier([FromRoute, Required] Guid dossierId, [FromBody, Required] CreateDossierDiscussionMessageDTO dossierMessageDTO)
    {
        var dossierMessage = _mapper.Map<DiscussionMessage>(dossierMessageDTO) ?? throw new InvalidInputException("Invalid Message");
        dossierMessage.DossierDiscussionId = dossierId;
        await _dossierReviewService.AddDossierDiscussionReview(dossierId, dossierMessage);
        return NoContent();
    }

    [HttpPut(nameof(EditReviewMessage) + "/{dossierId}")]
    [Authorize(Policies.IsOwnerOfDossier)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status200OK, "Review message successfully edited")]
    public async Task<ActionResult> EditReviewMessage([FromRoute, Required] Guid dossierId, [FromBody, Required] EditDossierDiscussionMessageDTO dossierMessageDTO)
    {
        await _dossierReviewService.EditDossierDiscussionReview(dossierId, dossierMessageDTO);
        return NoContent();
    }

    [HttpDelete(nameof(DeleteReviewMessage) + "/{dossierId}" + "/{messageId}")]
    [Authorize(Policies.IsOwnerOfDossier)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status200OK, "Review message successfully deleted")]
    public async Task<ActionResult> DeleteReviewMessage([FromRoute, Required] Guid dossierId, [FromRoute, Required] Guid messageId)
    {
        await _dossierReviewService.DeleteDossierDiscussionReview(dossierId, messageId);
        return NoContent();
    }
    
    [HttpPost(nameof(VoteReviewMessage) + "/{dossierId}")]
    [Authorize(Policies.IsOwnerOfDossier)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status200OK, "Vote successfully registered")]
    public async Task<ActionResult> VoteReviewMessage([FromRoute, Required] Guid dossierId, [FromBody, Required] VoteDossierDiscussionMessageDTO voteDossierDiscussionMessageDTO)
    {
        await _dossierReviewService.VoteDossierDiscussionMessage(dossierId, voteDossierDiscussionMessageDTO);
        return NoContent();
    }
}
