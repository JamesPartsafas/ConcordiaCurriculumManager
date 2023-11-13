namespace ConcordiaCurriculumManager.DTO.Dossiers.DossierReview;

public class DossierSubmissionDTO
{
    public required Guid DossierId { get; set; }

    public required IList<Guid> GroupIds { get; set; }
}
