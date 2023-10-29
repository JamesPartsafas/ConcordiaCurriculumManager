using ConcordiaCurriculumManager.DTO.Dossiers.CourseRequests;
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
            CourseNotes = "Students will love this course",
            CreditValue = "6.5",
            PreReqs = "SOEN 490 previously or concurrently",
            Rationale = "This course will be lots of fun",
            ResourceImplication = "We will have to hire a new professor",
            Career = CourseCareerEnum.UGRD,
            EquivalentCourses = "",
            ComponentCodes = new Dictionary<ComponentCodeEnum, int?> { { ComponentCodeEnum.LEC, 3 }, { ComponentCodeEnum.LAB, 2 } },
            DossierId = Guid.NewGuid(),
            SupportingFiles = new Dictionary<string, string> { { "myfilename.pdf", "my file encoded to base64" } }
        };
    }
}
