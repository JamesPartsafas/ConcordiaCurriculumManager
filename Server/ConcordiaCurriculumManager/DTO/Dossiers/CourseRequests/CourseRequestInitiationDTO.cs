using ConcordiaCurriculumManager.Models.Curriculum;

namespace ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests;

public abstract class CourseRequestInitiationDTO : CourseInitiationDTO
{
    public required string Subject { get; set; }

    public required string Catalog { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public required string CourseNotes { get; set; }

    public required string CreditValue { get; set; }

    public required string PreReqs { get; set; }

    public required CourseCareerEnum Career { get; set; }

    public required string EquivalentCourses { get; set; }

    public required Dictionary<ComponentCodeEnum, int?> ComponentCodes { get; set; }

    public required Dictionary<string, string> SupportingFiles { get; set; }

    public abstract CourseStateEnum GetAssociatedCourseState();
}
