using ConcordiaCurriculumManager.Models.Users;

namespace ConcordiaCurriculumManager.Security;

public static class Claims
{
    public const string Id = "id";

    public const string Email = "email";

    public const string FirstName = "fName";

    public const string LastName = "lName";

    public const string Roles = "roles";

    public const string Iat = "iat";

    public const string Group = "group";

    public const string GroupMaster = "groupMaster";
}

public static class Policies
{
    public static string Initiator => RoleEnum.Initiator.ToString();
}
