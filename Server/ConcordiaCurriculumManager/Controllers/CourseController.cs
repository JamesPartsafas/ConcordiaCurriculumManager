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

    [HttpGet(nameof(GetAllCourseComponents))]
    [SwaggerResponse(StatusCodes.Status200OK, "Course components retrieved")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User is not authorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    public async Task<ActionResult> GetAllCourseComponents()
    {
        try
        {
            var courseComponents = await _courseService.GetCourseComponents();
            var response = courseComponents.Select(_mapper.Map<CourseComponentDTO>);
            return Ok(response);
        }
        catch (Exception e)
        {
            var message = "Unexpected error occured while retrieving course components";
            _logger.LogWarning($"${message}: {e.Message}");
            return Problem(
                title: message,
                detail: e.Message,
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}
