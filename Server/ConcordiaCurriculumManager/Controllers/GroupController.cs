using ConcordiaCurriculumManager.DTO;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using ConcordiaCurriculumManager.Security;
using System.ComponentModel.DataAnnotations;

namespace ConcordiaCurriculumManager.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class GroupController : Controller
{
    private readonly IGroupService _groupService;
    private readonly IUserAuthenticationService _userService;
    private readonly ILogger<GroupController> _logger;

    public GroupController(IGroupService groupService, IUserAuthenticationService userService, ILogger<GroupController> logger)
    {
        _groupService = groupService;
        _userService = userService;
        _logger = logger;
    }

    [HttpPost(nameof(CreateGroup))]
    [Authorize(Roles = RoleNames.Admin)]
    [SwaggerResponse(StatusCodes.Status201Created, "Group created successfully")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "An error occured while creating the group")]
    public async Task<IActionResult> CreateGroup([FromBody] GroupCreateDTO groupCreateDTO)
    {
        var group = new Group { Name = groupCreateDTO.Name };
        var result = await _groupService.CreateGroupAsync(group);

        if (result)
        {
            return CreatedAtAction(nameof(GetGroupById), new { id = group.Id }, group);
        }
        else
        {
            return StatusCode(500, "An error occured while creating the group");
        }
    }

    [HttpGet("{groupId}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Group retrieved successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Group not found")]
    public async Task<IActionResult> GetGroupById([FromRoute, Required] Guid groupId)
    {
        var group = await _groupService.GetGroupByIdAsync(groupId);
        if (group == null)
        {
            return NotFound();
        }
        return Ok(group);
    }

    [HttpGet(nameof(GetAllGroups))]
    [SwaggerResponse(StatusCodes.Status200OK, "All groups retrieved successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No groups found")]
    public async Task<IActionResult> GetAllGroups()
    {
        var groups = await _groupService.GetAllGroupsAsync();
        return Ok(groups);
    }

    [HttpPost("{groupId}/users/{userId}")]
    [Authorize(Policies.IsGroupMasterOrAdmin)]
    [SwaggerResponse(StatusCodes.Status200OK, "User added to group successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Error adding user to group")]
    public async Task<IActionResult> AddUserToGroup([FromRoute, Required] Guid groupId, [FromRoute, Required] Guid userId)
    {
        var result = await _groupService.AddUserToGroup(userId, groupId);
        if (result)
        {
            return Ok();
        }
        return NotFound();
    }

    [HttpDelete("{groupId}/users/{userId}")]
    [Authorize(Policies.IsGroupMasterOrAdmin)]
    [SwaggerResponse(StatusCodes.Status200OK, "User removed from group successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Error removing user from group")]
    public async Task<IActionResult> RemoveUserFromGroup([FromRoute, Required] Guid groupId, [FromRoute, Required] Guid userId)
    {
        var result = await _groupService.RemoveUserFromGroup(userId, groupId);
        if (result)
        {
            return Ok();
        }
        return NotFound();
    }

    [HttpPost("{groupId}/masters/{userId}")]
    [Authorize(Policies.IsGroupMasterOrAdmin)]
    [SwaggerResponse(StatusCodes.Status200OK, "User added to group masters successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User is not a member of this group or is already a group master")]
    public async Task<IActionResult> AddGroupMaster([FromRoute, Required] Guid groupId, [FromRoute, Required] Guid userId)
    {
        var result = await _groupService.AddGroupMaster(userId, groupId);
        if (result)
        {
            return Ok();
        }
        return NotFound();
    }

    [HttpDelete("{groupId}/masters/{userId}")]
    [Authorize(Policies.IsGroupMasterOrAdmin)]
    [SwaggerResponse(StatusCodes.Status200OK, "User removed from group masters successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User is not a member of this group or is not a group master")]
    public async Task<IActionResult> RemoveGroupMaster([FromRoute, Required] Guid groupId, [FromRoute, Required] Guid userId)
    {
        var result = await _groupService.RemoveGroupMaster(userId, groupId);
        if (result)
        {
            return Ok();
        }
        return NotFound();
    }
}
