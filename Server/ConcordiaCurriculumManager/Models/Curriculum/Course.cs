using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests;
using ConcordiaCurriculumManager.Models.Curriculum.Dossiers;
using NpgsqlTypes;
using System.Diagnostics.Contracts;

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

    public int? Version { get; set; }

    public required bool Published { get; set; }

    public ICollection<CourseCourseComponent>? CourseCourseComponents { get; set; }

    public ICollection<SupportingFile>? SupportingFiles { get; set; }

    public CourseCreationRequest? CourseCreationRequest { get; set; }

    public CourseModificationRequest? CourseModificationRequest { get; set; }

    public CourseDeletionRequest? CourseDeletionRequest { get; set; }

    // Self-reference related fields
    public ICollection<CourseReference> CourseReferenced { get; set; } = new List<CourseReference>();

    public ICollection<CourseReference> CourseReferencing { get; set; } = new List<CourseReference>();

    public bool IsCourseStateFinalized() => CourseState == CourseStateEnum.Accepted || CourseState == CourseStateEnum.Deleted;

    public void VerifyCourseIsValidOrThrow()
    {
        // Published courses must have final state and version
        if (Published)
        {
            if (IsCourseStateFinalized() && Version != null) return;

            throw new ArgumentException("The course is published but does not have an appropriate course state or version");
        }

        // Courses with final state must have version
        if (IsCourseStateFinalized())
        {
            if (Version != null) return;

            throw new ArgumentException("The course has a finalized state but does not have a version");
        }

        // Courses with non-final states must not have version
        if (Version == null) return;

        throw new ArgumentException("The course has a non-final state but it has a version");
    }

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

    public static Course CreateCourseFromDTOData(CourseRequestInitiationDTO initiation, int concordiaCourseId, int? version)
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
        var newId = Guid.NewGuid();
        return new Course
        {
            Id = newId,
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
            Version = null,
            Published = false,
            CourseCourseComponents = CourseCourseComponent.CloneForDeletionRequest(course.CourseCourseComponents, newId),
            SupportingFiles = course.SupportingFiles,
        };
    }

    public static Course CloneCourse(Course course)
    {
        var newId = Guid.NewGuid();
        return new Course
        {
            Id = newId,
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
            CourseState = course.CourseState,
            Version = course.Version,
            Published = course.Published,
            CourseCourseComponents = CourseCourseComponent.CloneForDeletionRequest(course.CourseCourseComponents, newId),
            SupportingFiles = course.SupportingFiles,
        };
    }

    public void MarkAsAccepted(int version) => MarkAsFinalized(version, CourseStateEnum.Accepted);

    public void MarkAsDeleted(int version) => MarkAsFinalized(version, CourseStateEnum.Deleted);

    public void MarkAsPublished() 
    { 
        Published = true;

        VerifyCourseIsValidOrThrow();
    }

    private void MarkAsFinalized(int version, CourseStateEnum state)
    {
        Version = version;
        CourseState = state;

        VerifyCourseIsValidOrThrow();
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
    Deleted
}
