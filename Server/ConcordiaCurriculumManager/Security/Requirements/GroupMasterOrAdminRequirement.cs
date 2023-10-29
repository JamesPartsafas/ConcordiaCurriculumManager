using Microsoft.AspNetCore.Authorization;

namespace ConcordiaCurriculumManager.Security.Requirements;

public class GroupMasterOrAdminRequirement : IAuthorizationRequirement
{
}
