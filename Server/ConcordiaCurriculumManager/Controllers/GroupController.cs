using ConcordiaCurriculumManager.DTO;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace ConcordiaCurriculumManager.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]

public class GroupController : Controller
{
    private readonly IGroupService _groupService;

    public GroupController(IGroupService groupService)
    {
        _groupService = groupService;
    }

    [HttpPost]
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

    [HttpGet("{id}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Group retrieved successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Group not found")]
    public async Task<IActionResult> GetGroupById(Guid id)
    {
        var group = await _groupService.GetGroupByIdAsync(id);
        if (group == null)
        {
            return NotFound();
        }
        return Ok(group);
    }

    [HttpGet("{id}")]
    [SwaggerResponse(StatusCodes.Status200OK, "All groups retrieved successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No groups found")]
    public async Task<IActionResult> GetAllGroups()
    {
        var groups = await _groupService.GetAllGroups();
        if (groups == null || !groups.Any())
        {
            return NotFound();
        }
        return Ok(groups);
    }

    [HttpPost("{groupId}/users/{userId}")]
    [Authorize(Roles = RoleNames.Admin)]
    [SwaggerResponse(StatusCodes.Status200OK, "User added to group successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Error adding user to group")]
    public async Task<IActionResult> AddUserToGroup(Guid groupId, Guid userId)
    {
        var result = await _groupService.AddUserToGroup(userId, groupId);
        if (result)
        {
            return Ok();
        }
        return NotFound();
    }

    [HttpDelete("{groupId}/users/{userId}")]
    [Authorize(Roles = RoleNames.Admin)]
    [SwaggerResponse(StatusCodes.Status200OK, "User removed from group successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Error removing user from group")]
    public async Task<IActionResult> RemoveUserFromGroup(Guid groupId, Guid userId)
    {
        var result = await _groupService.RemoveUserFromGroup(userId, groupId);
        if (result)
        {
            return Ok();
        }
        return NotFound();
    }
}
