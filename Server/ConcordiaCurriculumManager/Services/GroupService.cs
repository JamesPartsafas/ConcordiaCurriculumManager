using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.DTO;
using ConcordiaCurriculumManager.Filters.Exceptions;

namespace ConcordiaCurriculumManager.Services;

public interface IGroupService
{
    Task<Group> GetGroupByIdAsync(Guid id);
    Task<List<Group>> GetAllGroupsAsync();
    Task<bool> CreateGroupAsync(Group group);
    Task<bool> AddUserToGroup(Guid userId, Guid groupId);
    Task<bool> RemoveUserFromGroup(Guid userId, Guid groupId);
    Task<bool> AddGroupMaster(Guid userId, Guid groupId);
    Task<bool> RemoveGroupMaster(Guid userId, Guid groupId);
    Task<bool> IsGroupMaster(Guid userId, Guid groupId);
    Task<bool> IsGroupIdListValid(IList<Guid> groupIds);
    Task<bool> UpdateGroupAsync(Guid id, GroupCreateDTO groupDto);
    Task<bool> DeleteGroupAsync(Guid id);
    Task<List<string>> GetAllGroupMembersAndMastersEmails(Guid id);
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

    public async Task<Group> GetGroupByIdAsync(Guid id)
    {
        return await _groupRepository.GetGroupById(id) ?? throw new NotFoundException($"Group {id} does not exist");
    }

    public async Task<List<Group>> GetAllGroupsAsync()
    {
        return await _groupRepository.GetAllGroups();
    }

    public async Task<bool> AddUserToGroup(Guid userId, Guid groupId)
    {
        return await _groupRepository.AddUserToGroup(userId, groupId);
    }

    public async Task<bool> RemoveUserFromGroup(Guid userId, Guid groupId)
    {
        return await _groupRepository.RemoveUserFromGroup(userId, groupId); 
    }

    public async Task<bool> AddGroupMaster(Guid userId, Guid groupId)
    {
        return await _groupRepository.AddGroupMaster(userId, groupId);
    }

    public async Task<bool> RemoveGroupMaster(Guid userId, Guid groupId)
    {
        return await _groupRepository.RemoveGroupMaster(userId, groupId);
    }

    public async Task<bool> IsGroupMaster(Guid userId, Guid groupId)
    {
        var group = await _groupRepository.GetGroupById(groupId);
        return group is not null && group.GroupMasters.Any(gm => gm.Id == userId);
    }

    public async Task<bool> IsGroupIdListValid(IList<Guid> groupIds)
    {
        var validGroupIds = await _groupRepository.GetValidGroupIds();

        return groupIds.All(groupId => validGroupIds.Contains(groupId));
    }

    public async Task<bool> UpdateGroupAsync(Guid id, GroupCreateDTO groupDto)
    {
        var group = await _groupRepository.GetGroupById(id);
        if (group == null)
        {
            return false;
        }

        group.Name = groupDto.Name;
        return await _groupRepository.UpdateGroupAsync(group);
    }

    public async Task<bool> DeleteGroupAsync(Guid id)
    {
        return await _groupRepository.DeleteGroupAsync(id);
    }

    public async Task<List<string>> GetAllGroupMembersAndMastersEmails(Guid id)
    {
        var group = await _groupRepository.GetGroupById(id);

        if (group is null)
        {
            throw new BadRequestException($"Group with id={id} does not exist");
        }

        var emails = group.Members.Select(m => m.Email.Trim()).Union(group.GroupMasters.Select(m => m.Email.Trim())).Distinct().ToList();

        return emails;
    }
}
