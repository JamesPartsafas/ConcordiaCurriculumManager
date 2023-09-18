﻿using ConcordiaCurriculumManager.Models;
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

    public Task<User?> GetUserByEmail(string email) => _dbContext.Users
        .Where(user => string.Equals(user.Email.ToLower(), email.ToLower()))
        .Select(user => new User
        { 
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Password = user.Password,
            Roles = (List<Role>) user.Roles.Select(role => new Role
            {
                UserRole = role.UserRole
            }) 
        })
        .FirstOrDefaultAsync();

    public async Task<bool> SaveUser(User user)
    {
        await _dbContext.Users.AddAsync(user);
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }
}
