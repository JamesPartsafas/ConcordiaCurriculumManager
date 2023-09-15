using ConcordiaCurriculumManager.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;

namespace ConcordiaCurriculumManager.Security;

public class TokenValidation : JwtBearerEvents
{
    public override Task TokenValidated(TokenValidatedContext context)
    {
        if (context.SecurityToken is not JwtSecurityToken accessToken)
        {
            context.Fail("Authentication cannot be completed");
            return Task.CompletedTask;
        }

        var authorizationService = context.HttpContext.RequestServices.GetRequiredService<IUserAuthorizationService>();
        var isBlackListedToken = authorizationService.IsBlacklistedToken(accessToken.RawData);

        if (isBlackListedToken)
        {
            context.Fail("Authentication cannot be completed");
        }

        return Task.CompletedTask;
    }
}
