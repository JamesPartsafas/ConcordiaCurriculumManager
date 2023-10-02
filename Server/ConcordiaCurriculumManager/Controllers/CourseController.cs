using AutoMapper;
using ConcordiaCurriculumManager.DTO;
using ConcordiaCurriculumManager.DTO.Courses;
using ConcordiaCurriculumManager.DTO.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum.Dossier;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Services;
using ConcordiaCurriculumManager.Swagger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.ComponentModel.DataAnnotations;

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
    [Consumes("application/json")]
    [Authorize(Roles = RoleNames.Initiator)]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status201Created, "Course creation dossier created successfully", typeof(Guid))]
    [SwaggerRequestExample(typeof(CourseCreationInitiationDTO), typeof(CourseCreationInitiationDTOExample))]
    public async Task<ActionResult> InitiateCourseCreation([FromBody, Required] CourseCreationInitiationDTO initiation)
    {
        try
        {
            var user = await _userService.GetCurrentUser();
            var response = await _courseService.InitiateCourseCreation(initiation, user);

            return Created($"/{nameof(InitiateCourseCreation)}", response.Id);
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
}
