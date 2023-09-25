using ConcordiaCurriculumManager.Models.Curriculum.Dossier;
using NpgsqlTypes;

namespace ConcordiaCurriculumManager.Models.Curriculum;

public class Course : BaseModel
{
    public required int CourseID { get; set; }

    public required string Subject { get; set; }

    public required string Catalog { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public required string CreditValue { get; set; }

    public required string PreReqs { get; set; }

    public required CourseCareerEnum Career { get; set; }

    public string EquivalentCourses { get; set; }

    public required CourseStateEnum CourseState { get; set; }

    public required int Version { get; set; }

    public required bool Published { get; set; }

    public List<CourseComponent> CourseComponents { get; set; } = new();

    public CourseCreationDossier? CourseCreationDossier { get; set; }

    public CourseReference CourseReference { get; set; }
}

public enum CourseCareerEnum
{
    [PgName(nameof(CCCE))]
    CCCE,

    [PgName(nameof(COLL))]
    COLL,

    [PgName(nameof(GRAD))]
    GRAD,

    [PgName(nameof(PDEV))]
    PDEV,

    [PgName(nameof(UGRD))]
    UGRD
}

public enum CourseStateEnum
{
    [PgName(nameof(Accepted))]
    Accepted,

    [PgName(nameof(NewCourseProposal))]
    NewCourseProposal,

    [PgName(nameof(CourseChangeProposal))]
    CourseChangeProposal
}
