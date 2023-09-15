namespace ConcordiaCurriculumManager.DTO;

public class AuthenticationResponse
{
    public const string MediaType = "application/x.ccm.authentication.token+json;v=1";

    public required string AccessToken { get; set; }
}

