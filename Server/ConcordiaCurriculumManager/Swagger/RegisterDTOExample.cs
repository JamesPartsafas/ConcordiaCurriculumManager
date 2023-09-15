using ConcordiaCurriculumManager.DTO;
using Swashbuckle.AspNetCore.Filters;

namespace ConcordiaCurriculumManager.Swagger;

public class RegisterDTOExample : IExamplesProvider<RegisterDTO>
{
    public RegisterDTO GetExamples()
    {
        return new()
        {
            FirstName = "FirstNameExample",
            LastName = "LastNameExample",
            Email = "EmailExample@example.com",
            Password = "ExamplePassword123!",
        };
    }
}
