using ConcordiaCurriculumManager.Models.Users;
using NpgsqlTypes;

namespace ConcordiaCurriculumManager.Models.Curriculum.Dossiers.DossierReview;

public class ApprovalHistory : BaseModel
{
    public required Guid GroupId { get; set; }

    public Group? Group { get; set; }

    public required Guid DossierId { get; set; }

    public Dossier? Dossier { get; set; }

    public required Guid UserId { get; set; }

    public User? User { get; set; }

    public required int OrderIndex { get; set; }

    public required ActionEnum Action { get; set; }
}

public enum ActionEnum
{
    [PgName(nameof(Forward))]
    Forward,

    [PgName(nameof(Return))]
    Return,

    [PgName(nameof(Reject))]
    Reject,

    [PgName(nameof(Accept))]
    Accept,
}