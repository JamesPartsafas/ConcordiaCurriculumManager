using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ConcordiaCurriculumManager.Repositories;

public interface IGroupRepository
{
    ValueTask<Group?> GetGroupById(Guid id);
    Task<Group?> GetGroupByName(string name);
    Task<List<Group>> GetAllGroups();
    Task<bool> SaveGroup(Group group);
    Task<bool> AddUserToGroup(Guid userId, Guid groupId);
    Task<bool> RemoveUserFromGroup(Guid userId, Guid groupId);
}

public class GroupRepository : IGroupRepository
{
    private readonly CCMDbContext _dbContext;

    public GroupRepository(CCMDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ValueTask<Group?> GetGroupById(Guid id) => _dbContext.Groups.FindAsync(id);

    public Task<Group?> GetGroupByName(string name) => _dbContext.Groups.SingleOrDefaultAsync(group => string.Equals(group.Name.ToLower(), name.ToLower()));

    public Task<List<Group>> GetAllGroups() => _dbContext.Groups.ToListAsync();

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
}