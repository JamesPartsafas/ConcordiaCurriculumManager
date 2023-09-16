using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;

namespace ConcordiaCurriculumManagerTest.UnitTests.UtilityFunctions;

internal static class HttpContextUtil
{
    public static void MockHttpContextGetToken(Mock<IHttpContextAccessor> httpContextAccessorMock, string tokenValue, string tokenName = "access_token", string scheme = "Bearer")
    {
        var httpContext = new Mock<HttpContext>();
        var authenticationServiceMock = new Mock<IAuthenticationService>();
        httpContextAccessorMock.Setup(x => x.HttpContext)
            .Returns(httpContext.Object);

        httpContext
            .Setup(x => x.RequestServices.GetService(typeof(IAuthenticationService)))
            .Returns(authenticationServiceMock.Object);

        var authResult = AuthenticateResult.Success(
            new AuthenticationTicket(new ClaimsPrincipal(), scheme));

        authResult.Properties!.StoreTokens(new[]
        {
            new AuthenticationToken { Name = tokenName, Value = tokenValue }
        });

        authenticationServiceMock
            .Setup(x => x.AuthenticateAsync(httpContext.Object, It.IsAny<string>()))
            .ReturnsAsync(authResult);
    }
}
