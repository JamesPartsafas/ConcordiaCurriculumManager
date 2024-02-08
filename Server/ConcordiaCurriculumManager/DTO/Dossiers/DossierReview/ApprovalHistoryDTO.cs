using ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;
using ConcordiaCurriculumManager.Models.Users;

namespace ConcordiaCurriculumManager.DTO.Dossiers.DossierReview;

public class ApprovalHistoryDTO
{
    public required Guid GroupId { get; set; }

    public required GroupDTO Group { get; set; }

    public required User User { get; set; }

    public required int OrderIndex { get; set; }

    public required ActionEnum Action { get; set; }

    public required DateTime CreatedDate { get; set; }
}
