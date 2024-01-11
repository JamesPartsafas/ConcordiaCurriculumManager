using ConcordiaCurriculumManager.DTO;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using ConcordiaCurriculumManager.Security;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace ConcordiaCurriculumManager.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class GroupController : Controller
{
    private readonly IMapper _mapper;
    private readonly IGroupService _groupService;
    private readonly IUserAuthenticationService _userService;
    private readonly ILogger<GroupController> _logger;

    public GroupController(IGroupService groupService, IUserAuthenticationService userService, ILogger<GroupController> logger, IMapper mapper)
    {
        _groupService = groupService;
        _userService = userService;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpPost(nameof(CreateGroup))]
    [Authorize(Roles = RoleNames.Admin)]
    [SwaggerResponse(StatusCodes.Status201Created, "Group created successfully")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "An error occured while creating the group")]
    public async Task<IActionResult> CreateGroup([FromBody] GroupCreateDTO groupCreateDTO)
    {
        var group = new Group { Name = groupCreateDTO.Name };
        var result = await _groupService.CreateGroupAsync(group);

        if (!result)
        {
            throw new Exception("An error occured while creating a group");
        }

        return CreatedAtAction(nameof(CreateGroup), new { id = group.Id }, group);
    }

    [HttpGet("{groupId}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Group retrieved successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Group not found")]
    public async Task<IActionResult> GetGroupById([FromRoute, Required] Guid groupId)
    {
        var group = await _groupService.GetGroupByIdAsync(groupId);
        var groupDTO = _mapper.Map<GroupDTO>(group);
        return Ok(groupDTO);
    }

    [HttpGet(nameof(GetAllGroups))]
    [SwaggerResponse(StatusCodes.Status200OK, "All groups retrieved successfully")]
    public async Task<IActionResult> GetAllGroups()
    {
        var groups = await _groupService.GetAllGroupsAsync();
        var groupsDTO = _mapper.Map<List<GroupDTO>>(groups);
        return Ok(groupsDTO);
    }

    [HttpPost("{groupId}/users/{userId}")]
    [Authorize(Policies.IsGroupMasterOrAdmin)]
    [SwaggerResponse(StatusCodes.Status200OK, "User added to group successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Group or user do not exist")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error adding user to group")]
    public async Task<IActionResult> AddUserToGroup([FromRoute, Required] Guid groupId, [FromRoute, Required] Guid userId)
    {
        var result = await _groupService.AddUserToGroup(userId, groupId);
        if (!result)
        {
            throw new Exception($"Failed to add user {userId} to group {groupId}");
        }

        return Ok();
    }

    [HttpDelete("{groupId}/users/{userId}")]
    [Authorize(Policies.IsGroupMasterOrAdmin)]
    [SwaggerResponse(StatusCodes.Status200OK, "User removed from group successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Group or user do not exist")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error removing user from group")]
    public async Task<IActionResult> RemoveUserFromGroup([FromRoute, Required] Guid groupId, [FromRoute, Required] Guid userId)
    {
        var result = await _groupService.RemoveUserFromGroup(userId, groupId);
        if (!result)
        {
            throw new Exception($"Failed to remove user {userId} from group {groupId}");
        }

        return Ok();
    }

    [HttpPost("{groupId}/masters/{userId}")]
    [Authorize(Policies.IsGroupMasterOrAdmin)]
    [SwaggerResponse(StatusCodes.Status200OK, "User added to group masters successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Group or user do not exist")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error adding user as a group master")]
    public async Task<IActionResult> AddGroupMaster([FromRoute, Required] Guid groupId, [FromRoute, Required] Guid userId)
    {
        var result = await _groupService.AddGroupMaster(userId, groupId);
        if (!result)
        {
            throw new Exception($"Failed to add user {userId} to group {groupId} as group master");
        }

        return Ok();
    }

    [HttpDelete("{groupId}/masters/{userId}")]
    [Authorize(Policies.IsGroupMasterOrAdmin)]
    [SwaggerResponse(StatusCodes.Status200OK, "User removed from group masters successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Group or user do not exist")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Error removing user from group master")]
    public async Task<IActionResult> RemoveGroupMaster([FromRoute, Required] Guid groupId, [FromRoute, Required] Guid userId)
    {
        var result = await _groupService.RemoveGroupMaster(userId, groupId);
        if (!result)
        {
            throw new Exception($"Failed to remove user {userId} from group {groupId} as a group master");
        }

        return Ok();
    }

    [HttpPut("{groupId}")]
    [Authorize(Roles = RoleNames.Admin)]
    [SwaggerResponse(StatusCodes.Status200OK, "Group updated successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Group does not exist")]
    public async Task<IActionResult> UpdateGroup(Guid groupId, [FromBody] GroupCreateDTO groupDto)
    {
        var result = await _groupService.UpdateGroupAsync(groupId, groupDto);
        if (result)
        {
            return Ok();
        }

        return NotFound();
    }

    [HttpDelete("{groupId}")]
    [Authorize(Roles = RoleNames.Admin)]
    [SwaggerResponse(StatusCodes.Status200OK, "Group has been deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Group does not exist")]
    public async Task<IActionResult> DeleteGroup(Guid groupId)
    {
        var deleted = await _groupService.DeleteGroupAsync(groupId);
        if (deleted)
        {
            return Ok();
        }

        return NotFound();
    }

}
