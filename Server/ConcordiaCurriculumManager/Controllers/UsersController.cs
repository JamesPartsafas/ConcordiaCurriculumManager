using AutoMapper;
using ConcordiaCurriculumManager.DTO;
using ConcordiaCurriculumManager.Filters.Exceptions;
using ConcordiaCurriculumManager.Security;
using ConcordiaCurriculumManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace ConcordiaCurriculumManager.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UsersController : Controller
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public UsersController(IMapper mapper, IUserService userService)
    {
        _mapper = mapper;
        _userService = userService;
    }

    [HttpGet(nameof(GetAllUsersAsync))]
    [SwaggerResponse(StatusCodes.Status200OK, "Current page of users was retrieved")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User is not authorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    [Authorize(Policies.IsGroupMasterOrAdmin)]
    public async Task<ActionResult> GetAllUsersAsync([FromQuery] Guid? lastId)
    {
        var users = await _userService.GetAllUsersPageableAsync(lastId ?? Guid.Empty);
        var userDTOs = _mapper.Map<List<UserDTO>>(users);
        return Ok(userDTOs);
    }

    [HttpGet(nameof(SearchUsersByEmail))]
    [SwaggerResponse(StatusCodes.Status200OK, "Current page of users was retrieved by email")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User is not authorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    public async Task<ActionResult> SearchUsersByEmail([FromQuery] Guid? lastId, [FromQuery, Required] string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new InvalidInputException("Email cannot be null or white space");
        }

        var users = await _userService.GetUserLikeEmailAsync(lastId ?? Guid.Empty, email.Trim());
        var userDTOs = _mapper.Map<List<UserDTO>>(users);
        return Ok(userDTOs);
    }

    [HttpGet(nameof(searchUsersByFirstName))]
    [SwaggerResponse(StatusCodes.Status200OK, "Current page of users was retrieved by first name")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User is not authorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    public async Task<ActionResult> searchUsersByFirstName([FromQuery, Required] string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new InvalidInputException("First name cannot be null or white space");
        }

        var users = await _userService.GetUsersByFirstName(firstName.Trim());
        var userDTOs = _mapper.Map<List<UserDTO>>(users);
        return Ok(userDTOs);
    }

    [HttpGet(nameof(searchUsersByLastName))]
    [SwaggerResponse(StatusCodes.Status200OK, "Current page of users was retrieved by last name")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "User is not authorized")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unexpected error")]
    public async Task<ActionResult> searchUsersByLastName([FromQuery, Required] string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new InvalidInputException("Last name cannot be null or white space");
        }

        var users = await _userService.GetUsersByLastName(lastName.Trim());
        var userDTOs = _mapper.Map<List<UserDTO>>(users);
        return Ok(userDTOs);
    }

}
