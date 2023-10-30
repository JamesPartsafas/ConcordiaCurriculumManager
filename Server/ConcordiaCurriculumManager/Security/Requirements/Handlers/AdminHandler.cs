using ConcordiaCurriculumManager.Models.Users;
using ConcordiaCurriculumManager.Services;
using Microsoft.AspNetCore.Authorization;

namespace ConcordiaCurriculumManager.Security.Requirements.Handlers;

public class AdminHandler : IAuthorizationHandler
{
    private readonly ILogger<AdminHandler> _logger;
    private readonly IUserService _userService;

    public AdminHandler(ILogger<AdminHandler> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    public async Task HandleAsync(AuthorizationHandlerContext context)
    {
        var pendingRequirements = context.PendingRequirements.ToList();
        var result = pendingRequirements.Select(requirement => CheckIfAdmin(context, requirement));
        await Task.WhenAll(result);
    }

    private async Task CheckIfAdmin(AuthorizationHandlerContext context, IAuthorizationRequirement requirement)
    {
        if (requirement is not GroupMasterOrAdminRequirement)
        {
            return;
        }

        _logger.LogInformation("Evaluting AdminHandler");

        if (context.User.Identity is null || !context.User.Identity.IsAuthenticated)
        {
            context.Fail();
            return;
        }

        var id = context.User.Claims.FirstOrDefault(c => c.Type.Equals(Claims.Id));

        if (id is null || !Guid.TryParse(id.Value, out var parsedId))
        {
            _logger.LogWarning("User is authenticated without a valid Id claim");
            context.Fail();
            return;
        }

        var user = await _userService.GetUserById(parsedId);

        if (user is null)
        {
            _logger.LogWarning("User is not authenticated but does not exist in the database");
            context.Fail();
            return;
        }

        if (user.Roles.Any(r => r.UserRole.Equals(RoleEnum.Admin)))
        {
            context.Succeed(requirement);
        }

    }
}
