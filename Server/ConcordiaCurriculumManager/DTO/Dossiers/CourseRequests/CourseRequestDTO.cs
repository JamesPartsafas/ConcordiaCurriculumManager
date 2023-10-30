namespace ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests;

public abstract class CourseRequestDTO
{
    public required Guid Id { get; set; }

    public required Guid DossierId { get; set; }

    public required string Rationale { get; set; }

    public required string ResourceImplication { get; set; }

    public required string Comment { get; set; }
}
