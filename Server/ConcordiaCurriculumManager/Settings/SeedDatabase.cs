using ConcordiaCurriculumManager.Models;

namespace ConcordiaCurriculumManager.Settings;

public class SeedDatabase
{
    public const string SectionName = nameof(SeedDatabase);

    public bool SkipUserDatabaseSeed { get; set; } = true;

    public List<User> Users { get; set; } = new();
}
