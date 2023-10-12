using System.Net;
using DogApi.Dto;

namespace DogApi.Middlewares;

public class ExceptionsHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionsHandlingMiddleware> _logger;

    public ExceptionsHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionsHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (ArgumentNullException ex)
        {
            await HandleExceptionAsync(httpContext, ex.Message, HttpStatusCode.NotFound, ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            await HandleExceptionAsync(httpContext, ex.Message, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex.Message, HttpStatusCode.InternalServerError, "Internet server error");
        }
    }

    private async Task HandleExceptionAsync(
        HttpContext httpContext,
        string exError,
        HttpStatusCode httpStatusCode,
        string message)
    {
        _logger.LogError(exError);

        HttpResponse response = httpContext.Response;

        response.ContentType = "application/json";
        response.StatusCode = (int)httpStatusCode;

        ErrorDto errorDto = new()
        {
            Message = message,
            StatusCode = (int)httpStatusCode
        };

        string result = errorDto.ToString();

        await response.WriteAsJsonAsync(result);
    }
}