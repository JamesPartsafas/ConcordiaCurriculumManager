using ConcordiaCurriculumManager.Models.Metrics;
using ConcordiaCurriculumManager.Services;
using System.Diagnostics;

namespace ConcordiaCurriculumManager.Middleware;

public class HttpMetricsMiddleware
{
    private readonly RequestDelegate _next;

    public HttpMetricsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext, IMetricsService metricsService)
    {
        var controllerName = httpContext.Request.RouteValues["controller"]?.ToString();
        var actionName = httpContext.Request.RouteValues["action"]?.ToString();

        if (controllerName is null || actionName is null)
        {
            await _next(httpContext);
            return;
        }

        var timer = Stopwatch.StartNew();
        await _next(httpContext);
        var elapsedTime = timer.ElapsedTicks / (Stopwatch.Frequency / 1000L);
        var responseCode = httpContext.Response.StatusCode;

        var metric = new HttpMetric
        {
            Controller = controllerName,
            Endpoint = actionName,
            ResponseTimeMilliSecond = elapsedTime,
            ResponseStatusCode = responseCode
        };

        await metricsService.SaveHttpMetric(metric);
    }
}
