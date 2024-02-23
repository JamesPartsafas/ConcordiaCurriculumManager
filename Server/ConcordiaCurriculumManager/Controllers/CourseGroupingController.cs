using AutoMapper;
using ConcordiaCurriculumManager.DTO;
using ConcordiaCurriculumManager.DTO.CourseGrouping;
using ConcordiaCurriculumManager.DTO.Courses;
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

    [HttpPost(nameof(InitiateCourseGroupingCreation) + "/{dossierId}")]
    [Authorize(Policies.IsOwnerOfDossier)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status201Created, "Course grouping creation created successfully", typeof(CourseGroupingRequestDTO))]
    public async Task<ActionResult> InitiateCourseGroupingCreation([FromBody, Required] CourseGroupingCreationRequestDTO dto)
    {
        var grouping = await _courseGroupingService.InitiateCourseGroupingCreation(dto);
        var groupingDTO = _mapper.Map<CourseGroupingRequestDTO>(grouping);

        return Created($"/{nameof(InitiateCourseGroupingCreation)}", groupingDTO);
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

    [HttpPost(nameof(InitiateCourseGroupingDeletion) + "/{dossierId}")]
    [Authorize(Policies.IsOwnerOfDossier)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status201Created, "Course grouping deletion created successfully", typeof(CourseGroupingRequestDTO))]
    public async Task<ActionResult> InitiateCourseGroupingDeletion([FromBody, Required] CourseGroupingModificationRequestDTO dto)
    {
        var grouping = await _courseGroupingService.InitiateCourseGroupingDeletion(dto);
        var groupingDTO = _mapper.Map<CourseGroupingRequestDTO>(grouping);

        return Created($"/{nameof(InitiateCourseGroupingDeletion)}", groupingDTO);
    }

    [HttpPut(nameof(EditCourseGroupingCreation) + "/{dossierId}" + "/{requestId}")]
    [Authorize(Policies.IsOwnerOfDossier)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status201Created, "Course grouping creation edited successfully", typeof(CourseGroupingRequestDTO))]
    public async Task<ActionResult> EditCourseGroupingCreation([FromRoute, Required] Guid requestId, [FromBody, Required] CourseGroupingCreationRequestDTO dto)
    {
        var grouping = await _courseGroupingService.EditCourseGroupingCreation(requestId, dto);
        var groupingDTO = _mapper.Map<CourseGroupingRequestDTO>(grouping);

        return Created($"/{nameof(EditCourseGroupingCreation)}", groupingDTO);
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

    [HttpPut(nameof(EditCourseGroupingDeletion) + "/{dossierId}" + "/{requestId}")]
    [Authorize(Policies.IsOwnerOfDossier)]
    [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Invalid input")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status201Created, "Course grouping deletion edited successfully", typeof(CourseGroupingRequestDTO))]
    public async Task<ActionResult> EditCourseGroupingDeletion([FromRoute, Required] Guid requestId, [FromBody, Required] CourseGroupingModificationRequestDTO dto)
    {
        var grouping = await _courseGroupingService.EditCourseGroupingDeletion(requestId, dto);
        var groupingDTO = _mapper.Map<CourseGroupingRequestDTO>(grouping);

        return Created($"/{nameof(EditCourseGroupingDeletion)}", groupingDTO);
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

    [HttpPut(nameof(PublishCourseGrouping) + "/{commonIdentifier}")]
    [Authorize(Policies.IsGroupMasterOrAdmin)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User is unauthorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [SwaggerResponse(StatusCodes.Status200OK, "Course Grouping has been published successfully", typeof(CourseGroupingDTO))]
    public async Task<ActionResult> PublishCourseGrouping([FromRoute, Required] Guid commonIdentifier)
    {
        var editedCourseGrouping = await _courseGroupingService.PublishCourseGrouping(commonIdentifier);
        var editedCourseGroupingDTO = _mapper.Map<CourseGroupingDTO>(editedCourseGrouping);

        return Ok(editedCourseGroupingDTO);
    }
}
