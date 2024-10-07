using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace Fruityvice.Api.ExceptionHandler;

public class ExceptionHandler
    (
    ILogger<ExceptionHandler> logger,
    IExceptionToResponseMapper exceptionToResponseMapper
    )
    : IExceptionHandler
{

    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception,
        CancellationToken cancellationToken)
    {

        const string contentType = "application/problem+json";
        context.Response.ContentType = contentType;
        var exceptionResponse = exceptionToResponseMapper.MapExceptionToResponse(exception);
        logger.LogError(new EventId(500, ""), exception.Message, exception?.InnerException ?? exception);
        context.Response.StatusCode = (int)(exceptionResponse?.StatusCode ?? HttpStatusCode.BadRequest);
        var response = exceptionResponse?.Response;
        if (response is { })
        {
            await context.Response.WriteAsync(JsonSerializer.Serialize(response), cancellationToken);
        }
        else
        {
            await context.Response.WriteAsync(JsonSerializer.Serialize(new ExceptionResponse(new { code = "error", reason = "There was an error." },
                   HttpStatusCode.InternalServerError)), cancellationToken);
        }
        return true;
    }
}
