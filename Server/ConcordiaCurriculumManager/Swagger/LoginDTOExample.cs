using ConcordiaCurriculumManager.DTO;
using Swashbuckle.AspNetCore.Filters;

namespace ConcordiaCurriculumManager.Swagger;

public class LoginDTOExample : IExamplesProvider<LoginDTO>
{
    public LoginDTO GetExamples()
    {
        return new()
        {
            Email = "EmailExample@example.com",
            Password = "ExamplePassword123!",
        };
    }
}
