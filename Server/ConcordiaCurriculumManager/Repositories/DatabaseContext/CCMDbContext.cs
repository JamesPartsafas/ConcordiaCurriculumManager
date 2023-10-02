using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Repositories.DatabaseContext.Seeding;
using ConcordiaCurriculumManager.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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

    public DbSet<Course> Courses { get; set; }

    public DbSet<CourseComponent> CourseComponents { get; set; }

    public DbSet<CourseCreationRequest> CourseCreationRequests { get; set; }

    public DbSet<Dossier> Dossiers { get; set; }

    public DbSet<CourseReference> CourseReferences { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ConfigureUserRoleRelationship(modelBuilder);
        PreseedUsersAndRolesInDatabase(modelBuilder);
        PreseedCoursesAndCourseComponentsInDatabase(modelBuilder);
        ConfigureDossiersRelationship(modelBuilder);
        ConfigureCourseReferencesRelationship(modelBuilder);
    }

    private static void ConfigureCourseReferencesRelationship(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>()
            .HasMany(c => c.CourseReferenced)
            .WithOne(cr => cr.CourseReferenced)
            .HasForeignKey(cr => cr.CourseReferencedId);

        modelBuilder.Entity<Course>()
           .HasMany(c => c.CourseReferencing)
           .WithOne(cr => cr.CourseReferencing)
           .HasForeignKey(cr => cr.CourseReferencingId);
    }

    private static void ConfigureUserRoleRelationship(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<RoleEnum>();

        modelBuilder.Entity<User>()
            .HasMany(user => user.Roles)
            .WithMany(role => role.Users);
    }

    private static void ConfigureDossiersRelationship(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>()
            .HasOne(course => course.CourseCreationRequest)
            .WithOne(request => request.NewCourse)
            .HasForeignKey<CourseCreationRequest>(dossier => dossier.NewCourseId);

        modelBuilder.Entity<User>()
          .HasMany(user => user.Dossiers)
          .WithOne(dossier => dossier.Initiator)
          .HasForeignKey(dossier => dossier.InitiatorId);

        modelBuilder.Entity<Dossier>()
            .HasMany(dossier => dossier.CourseCreationRequests)
            .WithOne(request => request.Dossier)
            .HasForeignKey(request => request.DossierId);
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

        modelBuilder.Entity<User>()
            .HasMany(user => user.Roles)
            .WithMany(role => role.Users)
            .UsingEntity(j => j.HasData(userRoles.Select(ur => new { ur.UsersId, ur.RolesId })));
    }

    private void PreseedCoursesAndCourseComponentsInDatabase(ModelBuilder modelBuilder)
    {
        var courseComponents = new List<CourseComponent>();

        foreach (KeyValuePair<ComponentCodeEnum, string> mapping in ComponentCodeMapping.GetComponentCodeMapping())
        {
            courseComponents.Add(new()
            {
                Id = Guid.NewGuid(),
                ComponentCode = mapping.Key,
                ComponentName = mapping.Value,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow
            });
        }

        modelBuilder.Entity<CourseComponent>()
            .HasData(courseComponents);

        if (_seedDatabase.SkipCourseDatabaseSeed)
        {
            return;
        }

        var courses = new List<Course>();
        var courseCourseComponents = new List<(Guid CoursesId, Guid CourseComponentsId)>();

        //new CourseSeeder().SeedCourseData(courses, courseCourseComponents, courseComponents); // TODO: Figure out how to run seeder without flooding migration script

        modelBuilder.Entity<Course>()
            .HasData(courses);

        modelBuilder.Entity<Course>()
            .HasMany(course => course.CourseComponents)
            .WithMany(courseComponent => courseComponent.Courses)
            .UsingEntity(j => j.HasData(courseCourseComponents.Select(ccc => new { ccc.CoursesId, ccc.CourseComponentsId })));
    }
}
