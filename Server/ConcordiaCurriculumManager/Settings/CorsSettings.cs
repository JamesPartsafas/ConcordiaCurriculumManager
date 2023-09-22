namespace ConcordiaCurriculumManager.Settings;

public class CorsSettings
{
    public const string SectionName = nameof(CorsSettings);

    public required string AllowedWebsite { get; set; }
}
