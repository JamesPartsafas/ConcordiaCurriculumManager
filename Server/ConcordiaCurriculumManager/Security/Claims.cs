using ConcordiaCurriculumManager.Models.Users;

namespace ConcordiaCurriculumManager.Security;

public static class Claims
{
    public const string Email = "email";

    public const string FirstName = "fName";

    public const string LastName = "lName";

    public const string Roles = "roles";
}

public static class Policies
{
    public static string Initiator => RoleEnum.Initiator.ToString();
}
