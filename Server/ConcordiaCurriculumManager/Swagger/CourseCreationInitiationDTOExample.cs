using ConcordiaCurriculumManager.DTO.Dossiers;
using ConcordiaCurriculumManager.Models.Curriculum;
using Swashbuckle.AspNetCore.Filters;

namespace ConcordiaCurriculumManager.Swagger;

public class CourseCreationInitiationDTOExample : IExamplesProvider<CourseCreationInitiationDTO>
{
    public CourseCreationInitiationDTO GetExamples()
    {
        return new()
        {
            Subject = "SOEN",
            Catalog = "500",
            Title = "Super Capstone for Software Engineers",
            Description = "An advanced capstone project",
            CreditValue = "6.5",
            PreReqs = "SOEN 490 previously or concurrently",
            Career = CourseCareerEnum.UGRD,
            EquivalentCourses = "",
            ComponentCodes = new List<ComponentCodeEnum> { ComponentCodeEnum.LEC, ComponentCodeEnum.LAB }
        };
    }
}
