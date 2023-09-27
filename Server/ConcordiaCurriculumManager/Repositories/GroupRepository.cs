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
}