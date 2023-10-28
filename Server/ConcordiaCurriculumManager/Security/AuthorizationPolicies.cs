using ConcordiaCurriculumManager.Security.Requirements;
using ConcordiaCurriculumManager.Security.Requirements.Handlers;
using Microsoft.AspNetCore.Authorization;

namespace ConcordiaCurriculumManager.Security;

public static class AuthorizationPolicies
{
    public static void AddPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(Policies.IsGroupMasterOrAdmin, policy =>
        {
            policy.AddRequirements(new GroupMasterOrAdminRequirement())
            .RequireAuthenticatedUser()
            .Build();
        });

        options.AddPolicy(Policies.IsOwnerOfDossier, policy =>
        {
            policy.AddRequirements(new OwnerOfDossierRequirement())
            .RequireAuthenticatedUser()
            .Build();
        });
    }

    public static void AddAuthorizationHandlers(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizationHandler, AdminHandler>();
        services.AddScoped<IAuthorizationHandler, GroupMasterHandler>();
        services.AddScoped<IAuthorizationHandler, OwnerOfDossierHanlder>();
    }
}
