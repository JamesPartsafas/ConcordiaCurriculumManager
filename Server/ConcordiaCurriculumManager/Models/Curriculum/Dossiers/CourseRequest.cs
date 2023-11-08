namespace ConcordiaCurriculumManager.Models.Curriculum.Dossiers;

public abstract class CourseRequest : BaseModel
{
    public required Guid DossierId { get; set; }

    public Dossier? Dossier { get; set; }

    public required string Rationale { get; set; }

    public required string ResourceImplication { get; set; }

    public required string Comment { get; set; }
}
