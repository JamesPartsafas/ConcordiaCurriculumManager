using ConcordiaCurriculumManager.Models.Curriculum;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ConcordiaCurriculumManager.Repositories.DatabaseContext;

public class CCMDbContext : DbContext
{
    public CCMDbContext(DbContextOptions<CCMDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<Group> Groups { get; set; }

    public DbSet<Course> Courses { get; set; }

    public DbSet<CourseComponent> CourseComponents { get; set; }

    public DbSet<CourseCreationRequest> CourseCreationRequests { get; set; }

    public DbSet<CourseModificationRequest> CourseModificationRequests { get; set; }

    public DbSet<Dossier> Dossiers { get; set; }

    public DbSet<CourseReference> CourseReferences { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ConfigureUserRoleRelationship(modelBuilder);
        ConfigureDossiersRelationship(modelBuilder);
        ConfigureCourseReferencesRelationship(modelBuilder);
        ConfigureGroupUserRelationship(modelBuilder);
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
        modelBuilder.Entity<User>()
            .HasMany(user => user.Roles)
            .WithMany(role => role.Users);

        modelBuilder.Entity<User>()
            .HasIndex(user => user.Email)
            .IsUnique();
    }

    private static void ConfigureDossiersRelationship(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>()
            .HasOne(course => course.CourseCreationRequest)
            .WithOne(request => request.NewCourse)
            .HasForeignKey<CourseCreationRequest>(dossier => dossier.NewCourseId);

        modelBuilder.Entity<Course>()
            .HasOne(course => course.CourseModificationRequest)
            .WithOne(request => request.Course)
            .HasForeignKey<CourseModificationRequest>(dossier => dossier.CourseId);

        modelBuilder.Entity<User>()
          .HasMany(user => user.Dossiers)
          .WithOne(dossier => dossier.Initiator)
          .HasForeignKey(dossier => dossier.InitiatorId);

        modelBuilder.Entity<Dossier>()
            .HasMany(dossier => dossier.CourseCreationRequests)
            .WithOne(request => request.Dossier)
            .HasForeignKey(request => request.DossierId);
    }

    private static void ConfigureGroupUserRelationship(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Group>()
            .HasMany(group => group.Members)
            .WithMany(user => user.Groups);

        modelBuilder.Entity<Group>()
            .HasMany(group => group.GroupMasters)
            .WithMany(user => user.MasteredGroups);
    }
}