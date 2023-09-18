using AutoMapper;
using ConcordiaCurriculumManager.DTO;
using ConcordiaCurriculumManager.Services;
using ConcordiaCurriculumManager.Swagger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace ConcordiaCurriculumManager.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CourseController : Controller
{
    private readonly IMapper _mapper;
    private readonly ILogger<AuthenticationController> _logger;
    private readonly ICourseService _courseService;

    public CourseController(IMapper mapper, ILogger<AuthenticationController> logger, ICourseService courseService)
    {
        _mapper = mapper;
        _logger = logger;
        _courseService = courseService;
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
}
