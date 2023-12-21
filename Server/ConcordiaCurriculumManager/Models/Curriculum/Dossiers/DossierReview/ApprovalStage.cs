using ConcordiaCurriculumManager.Models.Users;

namespace ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;

public class ApprovalStage : BaseModel
{
    public required Guid GroupId { get; set; }

    public Group? Group { get; set; }

    public required Guid DossierId { get; set; }

    public Dossier? Dossier { get; set; }

    public required int StageIndex { get; set; }
    
    public required bool IsCurrentStage { get; set; }

    public required bool IsFinalStage { get; set; }

    public bool IsInitialStage() => StageIndex == 0;
}
