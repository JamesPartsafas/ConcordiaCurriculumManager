using ConcordiaCurriculumManager.Filters.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using Serilog.Context;
using System.Net;

namespace ConcordiaCurriculumManager.Filters;

public class ExceptionHandlerFilter : IExceptionFilter
{
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly ILogger<ExceptionHandlerFilter> _logger;

    public ExceptionHandlerFilter(IWebHostEnvironment hostEnvironment, ILogger<ExceptionHandlerFilter> logger)
    {
        _hostEnvironment = hostEnvironment;
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        var unhandledException = context.Exception;
        var unhandledExceptionType = unhandledException.GetType();
        var contextResult = new ContentResult();

        if (unhandledExceptionType == typeof(NotFoundException))
        {
            _logger.LogInformation(unhandledException, $"A NotFound exception was handled: {unhandledException.Message}");
            contextResult.StatusCode = Convert.ToInt32(HttpStatusCode.NotFound);
        }
        else if (unhandledExceptionType == typeof(InvalidInputException))
        {
            _logger.LogInformation(unhandledException, $"An InvalidInput exception was handled: {unhandledException.Message}");
            contextResult.StatusCode = Convert.ToInt32(HttpStatusCode.UnprocessableEntity);
        }
        else if (unhandledExceptionType == typeof(BadRequestException))
        {
            _logger.LogInformation(unhandledException, $"A BadRequest exception was handled: {unhandledException.Message}");
            contextResult.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
        }
        else
        {
            _logger.LogWarning(unhandledException, $"A {unhandledException.GetType().Name} exception was handled: {unhandledException.Message}");
            contextResult.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);
        }

        if (_hostEnvironment.IsDevelopment())
        {
            contextResult.Content = unhandledException.Message;
        }

        context.Result = contextResult;
        context.ExceptionHandled = true;
    }
}