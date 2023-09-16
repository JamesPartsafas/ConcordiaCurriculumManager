using ConcordiaCurriculumManager.Models;
using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace ConcordiaCurriculumManager.Repositories;

public interface IUserRepository
{
    ValueTask<User?> GetUserById(Guid id);
    Task<User?> GetUserByEmail(string email);
    Task<bool> SaveUser(User user);
}

public class UserRepository : IUserRepository
{
    private readonly CCMDbContext _dbContext;

    public UserRepository(CCMDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ValueTask<User?> GetUserById(Guid id) => _dbContext.Users.FindAsync(id);

    public Task<User?> GetUserByEmail(string email) => _dbContext.Users.SingleOrDefaultAsync(user => string.Equals(user.Email.ToLower(), email.ToLower()));

    public async Task<bool> SaveUser(User user)
    {
        await _dbContext.Users.AddAsync(user);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }
}
