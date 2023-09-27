using ConcordiaCurriculumManager.Models;
using ConcordiaCurriculumManager.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Options;
using System.Reflection.Emit;

namespace ConcordiaCurriculumManager.Repositories.DatabaseContext;

public class CCMDbContext : DbContext
{
    private readonly SeedDatabase _seedDatabase;

    public CCMDbContext(DbContextOptions<CCMDbContext> options, IOptions<SeedDatabase> seedDatabase) : base(options)
    {
        _seedDatabase = seedDatabase.Value ?? throw new ArgumentNullException("SeedDatabase cannot be null");
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<Group> Groups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ConfigureUserRoleRelationship(modelBuilder);
        PreseedUsersAndRolesInDatabase(modelBuilder);
    }

    private static void ConfigureUserRoleRelationship(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<RoleEnum>();

        modelBuilder.Entity<User>()
            .HasMany(user => user.Roles)
            .WithMany(role => role.Users);
    }

    private void PreseedUsersAndRolesInDatabase(ModelBuilder modelBuilder)
    {
        var roles = new List<Role>();

        foreach (var role in Enum.GetValues(typeof(RoleEnum)))
        {
            roles.Add(new()
            {
                Id = Guid.NewGuid(),
                UserRole = (RoleEnum)role,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow
            });
        }

        modelBuilder.Entity<Role>()
            .HasData(roles);

        if (_seedDatabase.SkipUserDatabaseSeed)
        {
            return;
        }

        // Workaround for an undocumented behavior: OnModelCreating is called twice during migration creation.
        // This loop is essential because EF can't automatically translate the M:N relationship into inserts,
        // requiring us to insert entities into each table separately.
        // Consequently, we can't simply use _seedDatabase.Users to seed users.
        // If we were to set every user in _seedDatabase.Users to have an empty roles list (to address the above issue),
        // during the second run of this method, every user in _seedDatabase.Users would have an empty role list,
        // resulting in an empty userRoles list that prevents seeding the RoleUser table.
        var users = new List<User>();
        var userRoles = new List<(Guid UsersId, Guid RolesId)>();
        foreach (var user in _seedDatabase.Users)
        {
            roles.Join(user.Roles, role => role.UserRole, uRole => uRole.UserRole, (role, _) => role)
                 .ToList()
                 .ForEach(role => userRoles.Add((user.Id, role.Id)));

            users.Add(new()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
            });
        }

        modelBuilder.Entity<User>()
            .HasData(users);

        var groups = new List<Group>();
        var userGroups = new List<(Guid UsersId, Guid GroupsId)>();
        foreach(var group in _seedDatabase.Groups)
        {
            groups.Add(new()
            {
                Id = group.Id,
                Name = group.Name,
                Members = users.Where(user => group.Members.Contains(user)).ToList()
            });
        }

        modelBuilder.Entity<Group>()
            .HasData(groups);

        modelBuilder.Entity<User>()
            .HasMany(user => user.Roles)
            .WithMany(role => role.Users)
            .UsingEntity(j => j.HasData(userRoles.Select(ur => new { ur.UsersId, ur.RolesId })));

        modelBuilder.Entity<User>()
            .HasMany(user => user.Groups)
            .WithMany(group => group.Members)
            .UsingEntity(j => j.HasData(userGroups.Select(ug => new { UserId = ug.UsersId, GroupId = ug.GroupsId })));
    }
}
