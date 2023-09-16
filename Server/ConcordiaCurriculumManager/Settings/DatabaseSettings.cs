namespace ConcordiaCurriculumManager.Settings;

public class DatabaseSettings
{
    public const string SectionName = nameof(DatabaseSettings);

    public required string ConnectionString { get; set; }
}
