using AutoMapper;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories;
using ConcordiaCurriculumManager.DTO;

namespace ConcordiaCurriculumManager.Services;

public interface IGroupService
{
    Task<GroupDTO?> GetGroupByIdAsync(Guid id);
    Task<List<GroupDTO>> GetAllGroupsAsync();
    Task<bool> CreateGroupAsync(Group group);
    Task<bool> AddUserToGroup(Guid userId, Guid groupId);
    Task<bool> RemoveUserFromGroup(Guid userId, Guid groupId);
    Task<bool> AddGroupMaster(Guid userId, Guid groupId);
    Task<bool> RemoveGroupMaster(Guid userId, Guid groupId);
    Task<bool> IsGroupMaster(Guid userId, Guid groupId);

}

public class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepository;
    private readonly IMapper _mapper;

    public GroupService(IGroupRepository groupRepository, IMapper mapper)
    {
        _groupRepository = groupRepository;
        _mapper = mapper;
    }

    public async Task<bool> CreateGroupAsync(Group group)
    {
        return await _groupRepository.SaveGroup(group);
    }

    public async Task<GroupDTO?> GetGroupByIdAsync(Guid id)
    {
        var group = await _groupRepository.GetGroupById(id);
        if (group == null)
        {
            return null;
        }
        else
        {
            return _mapper.Map<GroupDTO>(group);
        }
    }

    public async Task<List<GroupDTO>> GetAllGroupsAsync()
    {
        var groups = await _groupRepository.GetAllGroups();
        if (groups == null || !groups.Any())
        {
            return new List<GroupDTO>();
        }
        else
        {
            return _mapper.Map<List<GroupDTO>>(groups);
        }
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
}
