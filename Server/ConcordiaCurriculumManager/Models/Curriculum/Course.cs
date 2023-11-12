using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
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

    public string? EquivalentCourses { get; set; }

    public string? CourseNotes { get; set; }

    public required CourseStateEnum CourseState { get; set; }

    public required int Version { get; set; }

    public required bool Published { get; set; }

    public ICollection<CourseCourseComponent>? CourseCourseComponents { get; set; }

    public ICollection<SupportingFile>? SupportingFiles { get; set; }

    public CourseCreationRequest? CourseCreationRequest { get; set; }

    public CourseModificationRequest? CourseModificationRequest { get; set; }

    public CourseDeletionRequest? CourseDeletionRequest { get; set; }

    // Self-reference related fields
    public ICollection<CourseReference>? CourseReferenced { get; set; }

    public ICollection<CourseReference>? CourseReferencing { get; set; }

    public void ModifyCourseFromDTOData(CourseInitiationBaseDataDTO initiation)
    {
        Title = initiation.Title;
        Description = initiation.Description;
        CourseNotes = initiation.CourseNotes;
        CreditValue = initiation.CreditValue;
        PreReqs = initiation.PreReqs;
        Career = initiation.Career;
        EquivalentCourses = initiation.EquivalentCourses;
        CourseCourseComponents = CourseCourseComponent.GetComponentCodeMapping(initiation.ComponentCodes, Id);
        SupportingFiles = SupportingFile.GetSupportingFileMapping(initiation.SupportingFiles, Id);
    }

    public static Course CreateCourseFromDTOData(CourseRequestInitiationDTO initiation, int concordiaCourseId, int version)
    {
        var internalId = Guid.NewGuid();
        return new Course
        {
            Id = internalId,
            CourseID = concordiaCourseId,
            Subject = initiation.Subject,
            Catalog = initiation.Catalog,
            Title = initiation.Title,
            Description = initiation.Description,
            CourseNotes = initiation.CourseNotes,
            CreditValue = initiation.CreditValue,
            PreReqs = initiation.PreReqs,
            Career = initiation.Career,
            EquivalentCourses = initiation.EquivalentCourses,
            CourseState = initiation.GetAssociatedCourseState(),
            Version = version,
            Published = false,
            CourseCourseComponents = CourseCourseComponent.GetComponentCodeMapping(initiation.ComponentCodes, internalId),
            SupportingFiles = SupportingFile.GetSupportingFileMapping(initiation.SupportingFiles, internalId)
        };
    }

    public static Course CloneCourseForDeletionRequest(Course course)
    {
        return new Course
        {
            Id = Guid.NewGuid(),
            CourseID = course.CourseID,
            Subject = course.Subject,
            Catalog = course.Catalog,
            Title = course.Title,
            Description = course.Description,
            CreditValue = course.CreditValue,
            PreReqs = course.PreReqs,
            Career = course.Career,
            EquivalentCourses = course.EquivalentCourses,
            CourseNotes = course.CourseNotes,
            CourseState = CourseStateEnum.CourseDeletionProposal,
            Version = course.Version + 1,
            Published = course.Published,
            CourseCourseComponents = course.CourseCourseComponents,
            SupportingFiles = course.SupportingFiles,
        };
    }
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
    CourseChangeProposal,

    [PgName(nameof(CourseDeletionProposal))]
    CourseDeletionProposal,

    [PgName(nameof(Deleted))]
    Deleted,

    [PgName(nameof(Rejected))]
    Rejected,
}
