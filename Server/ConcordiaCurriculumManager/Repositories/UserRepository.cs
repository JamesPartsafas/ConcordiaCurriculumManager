using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories.DatabaseContext;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;

namespace ConcordiaCurriculumManager.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserById(Guid id);
    Task<User?> GetUserByEmail(string email);
    Task<bool> SaveUser(User user);
    Task<bool> UpdateUser(User user);
    Task<IList<User>> GetAllUsersPageable(Guid id);
    Task<IList<User>> GetUsersLikeEmailPageable(Guid id, string email);
    Task<IList<User>> GetUsersByFirstName(string firstName);
    Task<IList<User>> GetUsersByLastName(string lastName);
    Task<int> SavePasswordResetToken(Guid? token, string email);
    Task<User?> GetUserByResetPasswordToken(Guid token);
}

public class UserRepository : IUserRepository
{
    private readonly CCMDbContext _dbContext;

    public UserRepository(CCMDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<User?> GetUserById(Guid id) => _dbContext.Users
        .Where(user => Equals(user.Id, id))
        .Select(ObjectSelectors.UserSelector())
        .FirstOrDefaultAsync();

    public Task<User?> GetUserByEmail(string email) => _dbContext.Users
        .Where(user => string.Equals(user.Email.ToLower(), email.ToLower()))
        .Include(user => user.Roles)
        .Include(user => user.Groups)
        .Include(user => user.MasteredGroups)
        .FirstOrDefaultAsync();

    public async Task<bool> SaveUser(User user)
    {
        await _dbContext.Users.AddAsync(user);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> UpdateUser(User user)
    {
        _dbContext.Users.Update(user);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public async Task<IList<User>> GetAllUsersPageable(Guid id) => await _dbContext.Users
        .OrderBy(u => u.Id)
        .Where(u => u.Id > id)
        .Take(10)
        .Select(ObjectSelectors.UserSelector())
        .ToListAsync();

    public async Task<IList<User>> GetUsersLikeEmailPageable(Guid id, string email) => await _dbContext.Users
        .OrderBy(u => u.Id)
        .Where(u => u.Id > id && u.Email.Contains(email))
        .Take(10)
        .Select(ObjectSelectors.UserSelector())
        .ToListAsync();

    public async Task<IList<User>> GetUsersByFirstName(string firstName) => await _dbContext.Users
        .OrderBy(u => u.Id)
        .Where(u => u.FirstName.ToLower().Contains(firstName.ToLower())) 
        .Take(10)
        .Select(ObjectSelectors.UserSelector())
        .ToListAsync();

    public async Task<IList<User>> GetUsersByLastName(string lastName) => await _dbContext.Users
        .OrderBy(u => u.Id)
        .Where(u => u.LastName.ToLower().Contains(lastName.ToLower())) 
        .Take(10)
        .Select(ObjectSelectors.UserSelector())
        .ToListAsync();

    public async Task<int> SavePasswordResetToken(Guid? token, string email) 
    {
        return await _dbContext.Users.Where(user => user.Email.Equals(email)).ExecuteUpdateAsync(b => b.SetProperty(user => user.ResetPasswordToken, token));
    }

    public Task<User?> GetUserByResetPasswordToken(Guid token) => _dbContext.Users
    .Where(user => user.ResetPasswordToken == token)
    .FirstOrDefaultAsync();
}
