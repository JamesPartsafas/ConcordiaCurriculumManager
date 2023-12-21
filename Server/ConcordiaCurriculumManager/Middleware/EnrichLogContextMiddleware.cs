using ConcordiaCurriculumManager.Security;

namespace ConcordiaCurriculumManager.Filters;

// This needs to a be a middleware not a filter so that the user Id
// is still logged when an exception is handled with the exception handler
// filter. For more info, check the order of executions of middleware and filters.
public class EnrichLogContextMiddleware
{
    private readonly RequestDelegate _next;

    public EnrichLogContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        var userIdAsString = "Anonymous";

        if (httpContext.User.Identity is not null && httpContext.User.Identity.IsAuthenticated)
        {
            var userId = httpContext.User.Claims.FirstOrDefault(c => c.Type.Equals(Claims.Id));
            if (userId is not null && Guid.TryParse(userId.Value, out var parsedUserId))
            {
                userIdAsString = parsedUserId.ToString();
            }
        }

        using (LogContext.PushProperty("UserId", userIdAsString)) {
            await _next(httpContext);
        }
    }
}