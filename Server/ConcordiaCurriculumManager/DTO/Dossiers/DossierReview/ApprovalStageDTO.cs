namespace ConcordiaCurriculumManager.DTO.Dossiers.DossierReview;

public class ApprovalStageDTO
{
    public required Guid GroupId { get; set; }

    public required GroupDTO Group { get; set; }

    public required int StageIndex { get; set; }

    public required bool IsCurrentStage { get; set; }

    public required bool IsFinalStage { get; set; }
}
