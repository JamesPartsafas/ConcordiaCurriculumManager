using ConcordiaCurriculumManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcordiaCurriculumManager.Repositories.DatabaseContext;

public class CCMDbContext : DbContext
{
    public CCMDbContext(DbContextOptions<CCMDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ConfigureUserRoleRelationship(modelBuilder);
    }

    private static void ConfigureUserRoleRelationship(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<RoleEnum>();

        modelBuilder.Entity<User>()
            .HasMany(user => user.Roles)
            .WithMany(role => role.Users);

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
    }

}
