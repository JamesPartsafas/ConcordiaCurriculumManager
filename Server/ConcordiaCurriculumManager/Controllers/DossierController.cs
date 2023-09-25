using AutoMapper;
using ConcordiaCurriculumManager.DTO.Dossiers;
using ConcordiaCurriculumManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;


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
    public async Task<ActionResult> GetDossiersByID() {
        try
        {
            Guid userId = (await _userService.GetCurrentUser()).Id;
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
}