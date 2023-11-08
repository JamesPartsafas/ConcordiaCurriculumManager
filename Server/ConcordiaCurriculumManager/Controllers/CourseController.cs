using AutoMapper;
using ConcordiaCurriculumManager.DTO;
using ConcordiaCurriculumManager.DTO.Courses;
using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Security;
using ConcordiaCurriculumManager.Services;
using ConcordiaCurriculumManager.Swagger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace ConcordiaCurriculumManager.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CourseController : Controller
{
    private readonly IMapper _mapper;
    private readonly ILogger<CourseController> _logger;
    private readonly ICourseService _courseService;
    private readonly IUserAuthenticationService _userService;

    public CourseController(IMapper mapper, ILogger<CourseController> logger, ICourseService courseService, IUserAuthenticationService userService)
    {
        _mapper = mapper;
        _logger = logger;
        _courseService = courseService;
        _userService = userService;
    }

    [HttpGet(nameof(GetAllCourseSettings))]
    [SwaggerResponse(StatusCodes.Status200OK, "Course settings retrieved")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User is not authorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    public async Task<ActionResult> GetAllCourseSettings()
    {
        try
        {
            var courseComponents = _courseService.GetAllCourseComponents();
            var courseCareers = _courseService.GetAllCourseCareers();
            var courseSubjects = await _courseService.GetAllCourseSubjects();

            var response = new CourseSettingsDTO 
            {
                CourseCareers = courseCareers,
                CourseComponents = courseComponents,
                CourseSubjects = courseSubjects
            };
            return Ok(response);
        }
        catch (Exception e)
        {
            var message = "Unexpected error occured while retrieving course settings";
            _logger.LogWarning($"${message}: {e.Message}");
            return Problem(
                title: message,
                detail: e.Message,
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost(nameof(InitiateCourseCreation))]
    [Consumes(typeof(CourseCreationInitiationDTO), MediaTypeNames.Application.Json)]
    [Authorize(Roles = RoleNames.Initiator)]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status201Created, "Course creation dossier created successfully", typeof(CourseCreationRequestDTO))]
    [SwaggerRequestExample(typeof(CourseCreationInitiationDTO), typeof(CourseCreationInitiationDTOExample))]
    public async Task<ActionResult> InitiateCourseCreation([FromBody, Required] CourseCreationInitiationDTO initiation)
    {
        try
        {
            Guid userId = Guid.Parse(_userService.GetCurrentUserClaim(Claims.Id));
            var courseCreationRequest = await _courseService.InitiateCourseCreation(initiation, userId);
            var courseCreationRequestDTO = _mapper.Map<CourseCreationRequestDTO>(courseCreationRequest);

            return Created($"/{nameof(InitiateCourseCreation)}", courseCreationRequestDTO);
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
            _logger.LogWarning($"Unexpected error occured while creating the new course dossier: {e.Message}");
            return Problem(
                title: "Unexpected error occured while creating the new course dossier",
                detail: e.Message,
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost(nameof(InitiateCourseModification))]
    [Consumes(typeof(CourseModificationInitiationDTO), MediaTypeNames.Application.Json)]
    [Authorize(Roles = RoleNames.Initiator)]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status201Created, "Course modification created successfully", typeof(CourseModificationRequestDTO))]
    public async Task<ActionResult> InitiateCourseModification([FromBody, Required] CourseModificationInitiationDTO modification)
    {
        try 
        {
            Guid userId = Guid.Parse(_userService.GetCurrentUserClaim(Claims.Id));
            var courseModificationRequest = await _courseService.InitiateCourseModification(modification, userId);
            var courseModificationRequestDTO = _mapper.Map<CourseModificationRequestDTO>(courseModificationRequest);

            return Created($"/{nameof(InitiateCourseModification)}", courseModificationRequestDTO);
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
            _logger.LogWarning($"Unexpected error occured while trying to modify the dossier: {e.Message}");
            return Problem(
                title: "Unexpected error occured while trying to modify the dossier",
                detail: e.Message,
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet(nameof(GetCourseData) + "/{subject}" + "/{catalog}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Course data retrieved")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User is not authorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    public async Task<ActionResult> GetCourseData([FromRoute, Required] string subject, string catalog) {
        try
        {
            var courseData = await _courseService.GetCourseData(subject, catalog);
            var courseDataDTOs = _mapper.Map<CourseDataDTO>(courseData);
            _logger.LogInformation(string.Join(",", courseDataDTOs));
            return Ok(courseDataDTOs);
        }
        catch (Exception e)
        {
            var message = "Unexpected error occured while retrieving the course data.";
            _logger.LogWarning($"${message}: {e.Message}");
            return Problem(
                title: message,
                detail: e.Message,
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost(nameof(InitiateCourseDeletion))]
    [Consumes(typeof(CourseDeletionInitiationDTO), MediaTypeNames.Application.Json)]
    [Authorize(Roles = RoleNames.Initiator)]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status201Created, "Course deletion created successfully", typeof(CourseDeletionRequestDTO))]
    public async Task<ActionResult> InitiateCourseDeletion([FromBody, Required] CourseDeletionInitiationDTO deletion)
    {
        try
        {
            Guid userId = Guid.Parse(_userService.GetCurrentUserClaim(Claims.Id));
            var courseDeletionRequest = await _courseService.InitiateCourseDeletion(deletion, userId);
            var courseDeletionRequestDTO = _mapper.Map<CourseDeletionRequestDTO>(courseDeletionRequest);

            return Created($"/{nameof(InitiateCourseDeletion)}", courseDeletionRequestDTO);
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
            _logger.LogWarning($"Unexpected error occured while trying to create the deletion request: {e.Message}");
            return Problem(
                title: "Unexpected error occured while trying to create the deletion request.",
                detail: e.Message,
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut(nameof(EditCourseCreationRequest) + "/{dossierId}")]
    [Authorize(Policies.IsOwnerOfDossier)]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status200OK, "Course creation request modified successfully", typeof(EditCourseCreationRequestDTO))]
    public async Task<ActionResult> EditCourseCreationRequest([FromBody, Required] EditCourseCreationRequestDTO edit)
    {
        try
        {
            var editedCourseCreatioRequest = await _courseService.EditCourseCreationRequest(edit);
            var editedCourseCreationRequestDTO = _mapper.Map<CourseCreationRequestDTO>(editedCourseCreatioRequest);

            return Ok(editedCourseCreationRequestDTO);
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
            _logger.LogWarning($"Unexpected error occured while trying to edit the course creation request: {e.Message}");
            return Problem(
                title: "Unexpected error occured while trying to edit the course creation request",
                detail: e.Message,
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }


    [HttpPut(nameof(EditCourseModificationRequest) + "/{dossierId}")]
    [Authorize(Policies.IsOwnerOfDossier)]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status200OK, "Course modfication request edited successfully", typeof(EditCourseModificationRequestDTO))]
    public async Task<ActionResult> EditCourseModificationRequest([FromBody, Required] EditCourseModificationRequestDTO edit)
    {
        try
        {
            var editedCourseModificationRequest = await _courseService.EditCourseModificationRequest(edit);
            var editedCourseModificationRequestDTO = _mapper.Map<CourseModificationRequestDTO>(editedCourseModificationRequest);

            return Ok(editedCourseModificationRequestDTO);
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
            _logger.LogWarning($"Unexpected error occured while trying to edit the course modification request: {e.Message}");
            return Problem(
                title: "Unexpected error occured while trying to to edit the course modification request",
                detail: e.Message,
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete(nameof(DeleteCourseCreationRequest) + "/{dossierId}" + "/{courseRequestId}")]
    [Authorize(Policies.IsOwnerOfDossier)]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Course creation request deleted successfully")]
    public async Task<ActionResult> DeleteCourseCreationRequest([FromRoute, Required] Guid courseRequestId)
    {
        try
        {
            await _courseService.DeleteCourseCreationRequest(courseRequestId);
            return NoContent();
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
            var message = "Unexpected error occured while deleting the course creation request.";
            _logger.LogWarning($"${message}: {e.Message}");
            return Problem(
                title: message,
                detail: e.Message,
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete(nameof(DeleteCourseModificationRequest) + "/{dossierId}" + "/{courseRequestId}")]
    [Authorize(Policies.IsOwnerOfDossier)]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Course modification request deleted successfully")]
    public async Task<ActionResult> DeleteCourseModificationRequest([FromRoute, Required] Guid courseRequestId)
    {
        try
        {
            await _courseService.DeleteCourseModificationRequest(courseRequestId);
            return NoContent();
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
            var message = "Unexpected error occured while deleting the course modification request.";
            _logger.LogWarning($"${message}: {e.Message}");
            return Problem(
                title: message,
                detail: e.Message,
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}
