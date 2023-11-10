using AutoMapper;
using ConcordiaCurriculumManager.DTO.Dossiers;
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
public class DossierController : Controller
{
    private readonly IMapper _mapper;
    private readonly ILogger<DossierController> _logger;
    private readonly IDossierService _dossierService;
    private readonly IUserAuthenticationService _userService;

    public DossierController(IMapper mapper, ILogger<DossierController> logger, IUserAuthenticationService userService, IDossierService dossierService)
    {
        _mapper = mapper;
        _logger = logger;
        _dossierService = dossierService;
        _userService = userService;
    }

    [HttpGet(nameof(GetDossiersByID))]
    [SwaggerResponse(StatusCodes.Status200OK, "Dossiers retrieved")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User is not authorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    public async Task<ActionResult> GetDossiersByID()
    {
        Guid userId = Guid.Parse(_userService.GetCurrentUserClaim(Claims.Id));
        var dossiers = await _dossierService.GetDossiersByID(userId);
        var dossiersDTOs = _mapper.Map<List<DossierDTO>>(dossiers);
        return Ok(dossiersDTOs);
    }

    [HttpGet("{dossierId}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Dossier retrieved successfully", typeof(DossierDetailsDTO))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Dossier not found")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    public async Task<IActionResult> GetDossierByDossierId([FromRoute, Required] Guid dossierId)
    {
        var dossier = await _dossierService.GetDossierDetailsById(dossierId);
        var dossierDetails = _mapper.Map<DossierDetailsDTO>(dossier);
        return Ok(dossierDetails);
    }

    [HttpPost(nameof(CreateDossierForUser))]
    [Authorize(Roles = RoleNames.Initiator)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status201Created, "Dossier created successfully", typeof(DossierDTO))]
    public async Task<ActionResult> CreateDossierForUser([FromBody, Required] CreateDossierDTO dossier)
    {
        var user = await _userService.GetCurrentUser();
        var createdDossier = await _dossierService.CreateDossierForUser(dossier, user);
        var createdDossierDTO = _mapper.Map<DossierDTO>(createdDossier);

        return Created($"/{nameof(CreateDossierForUser)}", createdDossierDTO);
    }

    [HttpPut(nameof(EditDossier) + "/{dossierId}")]
    [Authorize(Policies.IsOwnerOfDossier)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status201Created, "Dossier edited successfully", typeof(DossierDTO))]
    public async Task<ActionResult> EditDossier([FromRoute, Required] Guid dossierId, [FromBody, Required] EditDossierDTO dossier)
    {
        var editedDossier = await _dossierService.EditDossier(dossier, dossierId);
        var editedDossierDTO = _mapper.Map<DossierDTO>(editedDossier);

        return Created($"/{nameof(EditDossier)}", editedDossierDTO);
    }

    [HttpDelete(nameof(DeleteDossier) + "/{dossierId}")]
    [Authorize(Policies.IsOwnerOfDossier)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Dossier deleted successfully")]
    public async Task<ActionResult> DeleteDossier([FromRoute, Required] Guid dossierId)
    {
        await _dossierService.DeleteDossier(dossierId);
        return NoContent();
    }
}