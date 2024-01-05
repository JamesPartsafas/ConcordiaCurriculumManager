using AutoMapper;
using ConcordiaCurriculumManager.DTO.CourseGrouping;
using ConcordiaCurriculumManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace ConcordiaCurriculumManager.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class CourseGroupingController : Controller
{
    private readonly IMapper _mapper;
    private readonly ICourseGroupingService _courseGroupingService;

    public CourseGroupingController(ICourseGroupingService courseGroupingService, IMapper mapper)
    {
        _courseGroupingService = courseGroupingService;
        _mapper = mapper;
    }

    [HttpGet(nameof(GetCourseGrouping) + "/{courseGroupingId}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Course grouping data retrieved")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User is not authorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    public async Task<ActionResult> GetCourseGrouping([FromRoute, Required] Guid courseGroupingId)
    {
        var courseGrouping = await _courseGroupingService.GetCourseGrouping(courseGroupingId);
        var courseGroupingDTO = _mapper.Map<CourseGroupingDTO>(courseGrouping);

        return Ok(courseGroupingDTO);
    }
}
