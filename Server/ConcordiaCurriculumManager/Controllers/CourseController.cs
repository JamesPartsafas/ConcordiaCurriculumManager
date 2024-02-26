using AutoMapper;
using ConcordiaCurriculumManager.DTO.Courses;
using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests;
using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.InputDTOs;
using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.OutputDTOs;
using ConcordiaCurriculumManager.Models.Curriculum;
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
    private readonly IDossierService _dossierService;

    public CourseController(IMapper mapper, ILogger<CourseController> logger, ICourseService courseService, IUserAuthenticationService userService, IDossierService dossierService)
    {
        _mapper = mapper;
        _logger = logger;
        _courseService = courseService;
        _userService = userService;
        _dossierService = dossierService;
    }

    [HttpGet(nameof(GetAllCourseSettings))]
    [SwaggerResponse(StatusCodes.Status200OK, "Course settings retrieved")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User is not authorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    public async Task<ActionResult> GetAllCourseSettings()
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

    [HttpGet(nameof(GetCourseData) + "/{subject}" + "/{catalog}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Course data retrieved")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User is not authorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    public async Task<ActionResult> GetCourseData([FromRoute, Required] string subject, string catalog)
    {
        var courseData = await _courseService.GetCourseDataWithSupportingFilesOrThrowOnDeleted(subject, catalog);
        var courseDataDTOs = _mapper.Map<CourseDataDTO>(courseData);
        _logger.LogInformation(string.Join(",", courseDataDTOs));
        return Ok(courseDataDTOs);
    }

    [HttpPost(nameof(InitiateCourseCreation) + "/{dossierId}")]
    [Consumes(typeof(CourseCreationInitiationDTO), MediaTypeNames.Application.Json)]
    [Authorize(Policies.IsOwnerOfDossier)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status201Created, "Course creation dossier created successfully", typeof(CourseCreationRequestDTO))]
    [SwaggerRequestExample(typeof(CourseCreationInitiationDTO), typeof(CourseCreationInitiationDTOExample))]
    public async Task<ActionResult> InitiateCourseCreation([FromBody, Required] CourseCreationInitiationDTO initiation)
    {
        Guid userId = Guid.Parse(_userService.GetCurrentUserClaim(Claims.Id));
        var courseCreationRequest = await _courseService.InitiateCourseCreation(initiation, userId);
        var courseCreationRequestDTO = _mapper.Map<CourseCreationRequestDTO>(courseCreationRequest);

        return Created($"/{nameof(InitiateCourseCreation)}", courseCreationRequestDTO);
    }

    [HttpPost(nameof(InitiateCourseModification) + "/{dossierId}")]
    [Consumes(typeof(CourseModificationInitiationDTO), MediaTypeNames.Application.Json)]
    [Authorize(Policies.IsOwnerOfDossier)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status201Created, "Course modification created successfully", typeof(CourseModificationRequestDTO))]
    public async Task<ActionResult> InitiateCourseModification([FromBody, Required] CourseModificationInitiationDTO modification)
    {
        Guid userId = Guid.Parse(_userService.GetCurrentUserClaim(Claims.Id));
        var courseModificationRequest = await _courseService.InitiateCourseModification(modification, userId);
        var courseModificationRequestDTO = _mapper.Map<CourseModificationRequestDTO>(courseModificationRequest);

        return Created($"/{nameof(InitiateCourseModification)}", courseModificationRequestDTO);
    }

    [HttpPost(nameof(InitiateCourseDeletion) + "/{dossierId}")]
    [Consumes(typeof(CourseDeletionInitiationDTO), MediaTypeNames.Application.Json)]
    [Authorize(Policies.IsOwnerOfDossier)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status201Created, "Course deletion created successfully", typeof(CourseDeletionRequestDTO))]
    public async Task<ActionResult> InitiateCourseDeletion([FromBody, Required] CourseDeletionInitiationDTO deletion)
    {
        Guid userId = Guid.Parse(_userService.GetCurrentUserClaim(Claims.Id));
        var courseDeletionRequest = await _courseService.InitiateCourseDeletion(deletion, userId);
        var courseDeletionRequestDTO = _mapper.Map<CourseDeletionRequestDTO>(courseDeletionRequest);

        return Created($"/{nameof(InitiateCourseDeletion)}", courseDeletionRequestDTO);
    }

    [HttpPut(nameof(EditCourseCreationRequest) + "/{dossierId}")]
    [Authorize(Policies.IsOwnerOfDossier)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status200OK, "Course creation request modified successfully", typeof(EditCourseCreationRequestDTO))]
    public async Task<ActionResult> EditCourseCreationRequest([FromBody, Required] EditCourseCreationRequestDTO edit)
    {
        var editedCourseCreatioRequest = await _courseService.EditCourseCreationRequest(edit);
        var editedCourseCreationRequestDTO = _mapper.Map<CourseCreationRequestDTO>(editedCourseCreatioRequest);

        return Ok(editedCourseCreationRequestDTO);
    }


    [HttpPut(nameof(EditCourseModificationRequest) + "/{dossierId}")]
    [Authorize(Policies.IsOwnerOfDossier)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status200OK, "Course modfication request edited successfully", typeof(EditCourseModificationRequestDTO))]
    public async Task<ActionResult> EditCourseModificationRequest([FromBody, Required] EditCourseModificationRequestDTO edit)
    {
        var editedCourseModificationRequest = await _courseService.EditCourseModificationRequest(edit);
        var editedCourseModificationRequestDTO = _mapper.Map<CourseModificationRequestDTO>(editedCourseModificationRequest);

        return Ok(editedCourseModificationRequestDTO);
    }

    [HttpPut(nameof(EditCourseDeletionRequest) + "/{dossierId}")]
    [Authorize(Policies.IsOwnerOfDossier)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status200OK, "Course deletion request edited successfully", typeof(EditCourseDeletionRequestDTO))]
    public async Task<ActionResult> EditCourseDeletionRequest([FromBody, Required] EditCourseDeletionRequestDTO edit)
    {
        var editedCourseDeletionRequest = await _courseService.EditCourseDeletionRequest(edit);
        var editedCourseDeletionRequestDTO = _mapper.Map<CourseDeletionRequestDTO>(editedCourseDeletionRequest);

        return Ok(editedCourseDeletionRequestDTO);
    }

    [HttpDelete(nameof(DeleteCourseCreationRequest) + "/{dossierId}" + "/{courseRequestId}")]
    [Authorize(Policies.IsOwnerOfDossier)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Course creation request deleted successfully")]
    public async Task<ActionResult> DeleteCourseCreationRequest([FromRoute, Required] Guid courseRequestId)
    {
        await _courseService.DeleteCourseCreationRequest(courseRequestId);
        return NoContent();
    }

    [HttpDelete(nameof(DeleteCourseModificationRequest) + "/{dossierId}" + "/{courseRequestId}")]
    [Authorize(Policies.IsOwnerOfDossier)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Course modification request deleted successfully")]
    public async Task<ActionResult> DeleteCourseModificationRequest([FromRoute, Required] Guid courseRequestId)
    {
        await _courseService.DeleteCourseModificationRequest(courseRequestId);
        return NoContent();
    }

    [HttpDelete(nameof(DeleteCourseDeletionRequest) + "/{dossierId}" + "/{courseRequestId}")]
    [Authorize(Policies.IsOwnerOfDossier)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Course deletion request deleted successfully")]
    public async Task<ActionResult> DeleteCourseDeletionRequest([FromRoute, Required] Guid courseRequestId)
    {
        await _courseService.DeleteCourseDeletionRequest(courseRequestId);
        return NoContent();
    }

    [HttpGet(nameof(GetCourseCreationRequest) + "/{courseRequestId}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Course creation request retrieved")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User is not authorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    public async Task<ActionResult> GetCourseCreationRequest([FromRoute, Required] Guid courseRequestId)
    {
        var courseCreationRequest = await _dossierService.GetCourseCreationRequest(courseRequestId);
        var courseCreationRequestDTOs = _mapper.Map<CourseCreationRequestCourseDetailsDTO>(courseCreationRequest);
        _logger.LogInformation(string.Join(",", courseCreationRequestDTOs));
        return Ok(courseCreationRequestDTOs);
    }

    [HttpGet(nameof(GetCourseModificationRequest) + "/{courseRequestId}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Course modification request retrieved")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User is not authorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    public async Task<ActionResult> GetCourseModificationRequest([FromRoute, Required] Guid courseRequestId)
    {
        var courseModificationRequest = await _dossierService.GetCourseModificationRequest(courseRequestId);
        var courseModificationRequestDTOs = _mapper.Map<CourseModificationRequestCourseDetailsDTO>(courseModificationRequest);
        _logger.LogInformation(string.Join(",", courseModificationRequestDTOs));
        return Ok(courseModificationRequestDTOs);
    }

    [HttpGet(nameof(GetCourseDeletionRequest) + "/{courseRequestId}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Course deletion request retrieved")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User is not authorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    public async Task<ActionResult> GetCourseDeletionRequest([FromRoute, Required] Guid courseRequestId)
    {
        var courseDeletionRequest = await _dossierService.GetCourseDeletionRequest(courseRequestId);
        var courseDeletionRequestDTOs = _mapper.Map<CourseDeletionRequestDTO>(courseDeletionRequest);
        _logger.LogInformation(string.Join(",", courseDeletionRequestDTOs));
        return Ok(courseDeletionRequestDTOs);
    }

    [HttpGet("{id}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Course data retrieved", typeof(CourseDataDTO))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Course not found")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    public async Task<ActionResult<CourseDataDTO>> GetCourseById([FromRoute, Required] Guid id)
    {
        var course = await _courseService.GetCourseByIdAsync(id);
        if (course == null)
        {
            _logger.LogInformation($"Course with ID: {id} not found.");
            return NotFound();
        }

        var courseDetailsDto = _mapper.Map<CourseDataDTO>(course);
        return Ok(courseDetailsDto);
    }

    [HttpGet("BySubject/{subjectCode}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Courses for the specified subject retrieved", typeof(IEnumerable<CourseDataDTO>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Subject not found")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    public async Task<ActionResult<IEnumerable<CourseDataDTO>>> GetCoursesBySubject(string subjectCode)
    {
        var courses = await _courseService.GetCoursesBySubjectAsync(subjectCode);

        if (courses == null || !courses.Any())
        {
            return NotFound($"No courses found for subject: {subjectCode}");
        }

        var courseDtos = _mapper.Map<IEnumerable<CourseDataDTO>>(courses);
        return Ok(courseDtos);
    }

    [HttpPut(nameof(PublishCourse) + "/{subject}" + "/{catalog}")]
    [Authorize(Policies.IsGroupMasterOrAdmin)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User is unauthorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status200OK, "Course has been published successfully", typeof(CourseDataDTO))]
    public async Task<ActionResult> PublishCourse([FromRoute, Required] string subject, string catalog)
    {
        var editedCourse = await _courseService.PublishCourse(subject, catalog);
        var editedCourseDTO = _mapper.Map<CourseDataDTO>(editedCourse);

        return Ok(editedCourseDTO);
    }
}
