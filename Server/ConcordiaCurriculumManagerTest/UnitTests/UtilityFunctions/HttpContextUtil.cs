using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Collections.Specialized;
using System.Security.Claims;
using System.Linq;
using Microsoft.Extensions.Primitives;

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


    public static void MockHttpContextGetTokenWithUserToken(Mock<IHttpContextAccessor> httpContextAccessorMock, string tokenValue, IEnumerable<Claim> claims, string tokenName = "access_token", string scheme = "Bearer")
    {
        MockHttpContextGetToken(httpContextAccessorMock, tokenValue, tokenName, scheme);
        var user = new ClaimsPrincipal(new ClaimsIdentity(claims));
        httpContextAccessorMock.Setup(h => h.HttpContext!.User).Returns(user);
    }

    public static Mock<HttpContext> GetMockHttpContextWithQuery(IEnumerable<(string, string)> queries)
    {
        var queryDict = new Dictionary<string, StringValues>();
        queries.ToList().ForEach(query => queryDict.Add(query.Item1, query.Item2));

        var mockRequest = new Mock<HttpRequest>();
        mockRequest.Setup(r => r.Query).Returns(new QueryCollection(queryDict));
        var mockHttpContext = new Mock<HttpContext>();
        mockHttpContext.Setup(c => c.Request).Returns(mockRequest.Object);
        
        return mockHttpContext;

    }
}
