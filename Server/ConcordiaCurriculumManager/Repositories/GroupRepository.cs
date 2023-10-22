﻿using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ConcordiaCurriculumManager.Repositories;

public interface IGroupRepository
{
    Task<Group?> GetGroupById(Guid id);
    Task<List<Group>> GetAllGroups();
    Task<bool> SaveGroup(Group group);
    Task<bool> AddUserToGroup(Guid userId, Guid groupId);
    Task<bool> RemoveUserFromGroup(Guid userId, Guid groupId);
    Task<bool> AddGroupMaster(Guid userId, Guid groupId);
    Task<Group> GetGroupWithMasters(Guid groupId);
}

public class GroupRepository : IGroupRepository
{
    private readonly CCMDbContext _dbContext;

    public GroupRepository(CCMDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Group?> GetGroupById(Guid id) => _dbContext.Groups
        .Where(group => group.Id == id)
        .Select(ObjectSelectors.GroupSelector())
        .FirstOrDefaultAsync();

    public Task<List<Group>> GetAllGroups() => _dbContext.Groups
        .Select(ObjectSelectors.GroupSelector())
        .ToListAsync();
       
    public async Task<bool> SaveGroup(Group group)
    {
        await _dbContext.Groups.AddAsync(group);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> AddUserToGroup(Guid userId, Guid groupId)
    {
        var group = await _dbContext.Groups.FindAsync(groupId);
        var user = await _dbContext.Users.FindAsync(userId);
        if (group != null && user != null)
        {
            group.Members.Add(user);
            return await _dbContext.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<bool> RemoveUserFromGroup(Guid userId, Guid groupId)
    {
        var group = await _dbContext.Groups.Include(g => g.Members).FirstOrDefaultAsync(g => g.Id == groupId);
        var user = group?.Members.FirstOrDefault(u => u.Id == userId);
        if (group != null && user != null)
        {
            group.Members.Remove(user);
            return await _dbContext.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<bool> AddGroupMaster(Guid userId, Guid groupId)
    {
        var group = await _dbContext.Groups
                                    .Include(g => g.Members)
                                    .Include(g => g.GroupMasters)
                                    .FirstOrDefaultAsync(g => g.Id == groupId);

        if (group == null)
        {
            return false;
        }

        var member = group.Members.FirstOrDefault(u => u.Id == userId);
        if (member == null)
        {
            return false;
        }

        var isAlreadyGroupMaster = group.GroupMasters.Any(u => u.Id == userId);
        if (isAlreadyGroupMaster)
        {
            return false;
        }

        group.GroupMasters.Add(member);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<Group> GetGroupWithMasters(Guid groupId)
    {
        var groupWithMasters = await _dbContext.Groups
            .Include(g => g.GroupMasters)
            .Where(group => group.Id == groupId)
            .FirstOrDefaultAsync();

        return groupWithMasters;
    }
}