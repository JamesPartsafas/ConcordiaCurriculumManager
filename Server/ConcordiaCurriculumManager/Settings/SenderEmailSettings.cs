namespace ConcordiaCurriculumManager.Settings;

public class SenderEmailSettings
{
    public const string SectionName = nameof(SenderEmailSettings);

    public required string SenderSMTPHost { get; set; }

    public required int SenderSMTPPort { get; set; }

    public required string SenderEmail { get; set; }

    public required string SenderPassword { get; set; }
}
