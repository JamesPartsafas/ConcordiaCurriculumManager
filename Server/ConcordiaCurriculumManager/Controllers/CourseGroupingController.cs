using AutoMapper;
using ConcordiaCurriculumManager.DTO;
using ConcordiaCurriculumManager.DTO.CourseGrouping;
using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.CourseGroupingRequests;
using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.InputDTOs;
using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests.OutputDTOs;
using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Models.Curriculum.CourseGroupings;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Security;
using ConcordiaCurriculumManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

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

    [HttpGet(nameof(GetCourseGroupingsBySchool) + "/{school}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Course grouping data retrieved")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User is not authorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    public async Task<ActionResult> GetCourseGroupingsBySchool([FromRoute, Required] SchoolEnum school)
    {
        var courseGroupings = await _courseGroupingService.GetCourseGroupingsBySchoolNonRecursive(school);
        var courseGroupingDTOs = _mapper.Map<ICollection<CourseGroupingDTO>>(courseGroupings);

        return Ok(courseGroupingDTOs);
    }

    [HttpGet(nameof(SearchCourseGroupingsByName))]
    [SwaggerResponse(StatusCodes.Status200OK, "Course grouping data retrieved")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User is not authorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    public async Task<ActionResult> SearchCourseGroupingsByName([FromQuery, Required] string name)
    {
        var courseGroupings = await _courseGroupingService.GetCourseGroupingsLikeName(name.Trim());
        var courseGroupingDTOs = _mapper.Map<ICollection<CourseGroupingDTO>>(courseGroupings);

        return Ok(courseGroupingDTOs);
    }

    [HttpPost(nameof(InitiateCourseGroupingModification) + "/{dossierId}")]
    [Authorize(Policies.IsOwnerOfDossier)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status201Created, "Course grouping modification created successfully", typeof(CourseGroupingRequestDTO))]
    public async Task<ActionResult> InitiateCourseGroupingModification([FromBody, Required] CourseGroupingModificationRequestDTO dto)
    {
        var grouping = await _courseGroupingService.InitiateCourseGroupingModification(dto);
        var groupingDTO = _mapper.Map<CourseGroupingRequestDTO>(grouping);

        return Created($"/{nameof(InitiateCourseGroupingModification)}", groupingDTO);
    }

    [HttpPut(nameof(EditCourseGroupingModification) + "/{dossierId}" + "/{requestId}")]
    [Authorize(Policies.IsOwnerOfDossier)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status201Created, "Course grouping modification edited successfully", typeof(CourseGroupingRequestDTO))]
    public async Task<ActionResult> EditCourseGroupingModification([FromRoute, Required] Guid requestId, [FromBody, Required] CourseGroupingModificationRequestDTO dto)
    {
        var grouping = await _courseGroupingService.EditCourseGroupingModification(requestId, dto);
        var groupingDTO = _mapper.Map<CourseGroupingRequestDTO>(grouping);

        return Created($"/{nameof(EditCourseGroupingModification)}", groupingDTO);
    }

    [HttpDelete(nameof(DeleteCourseGroupingRequest) + "/{dossierId}" + "/{requestId}")]
    [Authorize(Policies.IsOwnerOfDossier)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Course grouping request deleted successfully")]
    public async Task<ActionResult> DeleteCourseGroupingRequest([FromRoute, Required] Guid dossierId, [FromRoute, Required] Guid requestId)
    {
        await _courseGroupingService.DeleteCourseGroupingRequest(dossierId, requestId);

        return NoContent();
    }
}
