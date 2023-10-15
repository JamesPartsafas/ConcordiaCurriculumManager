using AutoMapper;
using ConcordiaCurriculumManager.DTO.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Security;
using ConcordiaCurriculumManager.Services;
using ConcordiaCurriculumManager.Swagger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.ComponentModel.DataAnnotations;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        try
        {
            Guid userId = Guid.Parse(_userService.GetCurrentUserClaim(Claims.Id));
            var dossiers = await _dossierService.GetDossiersByID(userId);
            var dossiersDTOs = _mapper.Map<List<DossierDTO>>(dossiers);
            _logger.LogInformation(string.Join(",", dossiersDTOs));
            return Ok(dossiersDTOs);
        }
        catch (Exception e)
        {
            var message = "Unexpected error occured while retrieving the dossier.";
            _logger.LogWarning($"${message}: {e.Message}");
            return Problem(
                title: message,
                detail: e.Message,
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost(nameof(CreateDossierForUser))]
    [Authorize(Roles = RoleNames.Initiator)]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status201Created, "Dossier created successfully", typeof(DossierDTO))]
    public async Task<ActionResult> CreateDossierForUser([FromBody, Required] CreateDossierDTO dossier)
    {
        try
        {
            var user = await _userService.GetCurrentUser();
            var createdDossier = await _dossierService.CreateDossierForUser(dossier, user);
            var createdDossierDTO = _mapper.Map<DossierDTO>(createdDossier);

            return Created($"/{nameof(CreateDossierForUser)}", createdDossierDTO);
        }
        catch (ArgumentException e)
        {
            return Problem(
                title: "One or more validation errors occurred.",
                detail: e.Message,
                statusCode: StatusCodes.Status400BadRequest);
        }
        catch (Exception e)
        {
            var message = "Unexpected error occured while creating the dossier.";
            _logger.LogWarning($"${message}: {e.Message}");
            return Problem(
                title: message,
                detail: e.Message,
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut(nameof(EditDossier))]
    [Authorize(Roles = RoleNames.Initiator)]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status201Created, "Dossier edited successfully", typeof(DossierDTO))]
    public async Task<ActionResult> EditDossier([FromBody, Required] EditDossierDTO dossier) {
        try
        {
            var user = await _userService.GetCurrentUser();
            var editedDossier = await _dossierService.EditDossier(dossier, user);
            var editedDossierDTO = _mapper.Map<DossierDTO>(editedDossier);

            return Created($"/{nameof(EditDossier)}", editedDossierDTO);
        }
        catch (ArgumentException e)
        {
            return Problem(
                title: "One or more validation errors occurred.",
                detail: e.Message,
                statusCode: StatusCodes.Status400BadRequest);
        }
        catch (Exception e)
        {
            var message = "Unexpected error occured while editing the dossier.";
            _logger.LogWarning($"${message}: {e.Message}");
            return Problem(
                title: message,
                detail: e.Message,
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}