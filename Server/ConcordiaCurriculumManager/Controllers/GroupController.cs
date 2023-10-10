using ConcordiaCurriculumManager.DTO;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConcordiaCurriculumManager.Controllers;

[ApiController]
[Route("api/[controller]")]

public class GroupController : Controller
{
    private readonly IGroupService _groupService;

    public GroupController(IGroupService groupService)
    {
        _groupService = groupService;
    }

    [HttpPost]
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
    public async Task<IActionResult> GetGroupById(Guid id)
    {
        var group = await _groupService.GetGroupByIdAsync(id);
        if (group == null)
        {
            return NotFound();
        }
        return Ok(group);
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetGroupByName(String name)
    {
        var group = await _groupService.GetGroupByNameAsync(name);
        if (group == null)
        {
            return NotFound();
        }
        return Ok(group);
    }

    [HttpPost("{groupId}/users/{userId}")]
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
