namespace ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests;

public abstract class CourseInitiationDTO
{
    public required Guid DossierId { get; set; }

    public required string Rationale { get; set; }

    public required string ResourceImplication { get; set; }

    public string? Comment { get; set; }
}
