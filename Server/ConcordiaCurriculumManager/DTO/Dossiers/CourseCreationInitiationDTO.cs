using ConcordiaCurriculumManager.Models.Curriculum;

namespace ConcordiaCurriculumManager.DTO.Dossiers;

public class CourseCreationInitiationDTO
{
    public required string Subject { get; set; }

    public required string Catalog { get; set; }

    public required string Title { get; set; }

    public required string Description { get; set; }

    public required string CreditValue { get; set; }

    public required string PreReqs { get; set; }

    public required CourseCareerEnum Career { get; set; }

    public required string EquivalentCourses { get; set; }

    public required List<ComponentCodeEnum> ComponentCodes { get; set; }
}
