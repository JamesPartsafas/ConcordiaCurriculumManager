namespace ConcordiaCurriculumManager.Security;

public static class Policies
{
    public const string IsGroupMasterOrAdmin = nameof(IsGroupMasterOrAdmin);
    public const string IsOwnerOfDossier = nameof(IsOwnerOfDossier);
}
