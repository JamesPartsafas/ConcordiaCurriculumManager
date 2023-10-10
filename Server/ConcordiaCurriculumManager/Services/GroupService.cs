using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories;

namespace ConcordiaCurriculumManager.Services;

public interface IGroupService
{
    Task<Group?> GetGroupByIdAsync(Guid id);
    Task<Group?> GetGroupByNameAsync(string name);
    Task<bool> CreateGroupAsync(Group group);
    Task<bool> AddUserToGroup(Guid userId, Guid groupId);
    Task<bool> RemoveUserFromGroup(Guid userId, Guid groupId);
}

public class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepository;

    public GroupService(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<bool> CreateGroupAsync(Group group)
    {
        return await _groupRepository.SaveGroup(group);
    }

    public async Task<Group?> GetGroupByIdAsync(Guid id)
    {
        return await _groupRepository.GetGroupById(id);
    }

    public async Task<Group?> GetGroupByNameAsync(string name)
    {
        return await _groupRepository.GetGroupByName(name);
    }

    public async Task<bool> AddUserToGroup(Guid userId, Guid groupId)
    {
        return await _groupRepository.AddUserToGroup(userId, groupId);
    }

    public async Task<bool> RemoveUserFromGroup(Guid userId, Guid groupId)
    {
        return await _groupRepository.RemoveUserFromGroup(userId, groupId); 
    }
}
