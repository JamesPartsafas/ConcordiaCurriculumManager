namespace ConcordiaCurriculumManager.Settings;

public class IdentitySettings
{
    public const string SectionName = nameof(IdentitySettings);

    public required string Issuer { get; set; }

    public required string Audience { get; set; }

    public required string SecurityAlgorithms { get; set; }

    public required string Key { get; set; }
}
