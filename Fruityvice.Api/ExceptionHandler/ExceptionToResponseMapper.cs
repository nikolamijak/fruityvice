using Fruityvice.Api.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Security.Authentication;

namespace Fruityvice.Api.ExceptionHandler;

public class ExceptionToResponseMapper : IExceptionToResponseMapper
{
    public ExceptionResponse MapExceptionToResponse(Exception exception)
    => exception switch
    {


        CustomValidationException cve when cve.Failures is not null => new ExceptionResponse(new { code = "validation_error", reason = cve.Failures }, HttpStatusCode.BadRequest),
        CustomValidationException cve when cve.Failures is null => new ExceptionResponse(new { code = "validation_error", reason = cve.ErrorMessage }, HttpStatusCode.BadRequest),
        NotFoundException nfe => new ExceptionResponse(new { code = "resource_not_found", reason = $"{nfe.Message}" }, HttpStatusCode.NotFound),
        ApiIntegrationException apiIntegrationException when apiIntegrationException.StatusCode.HasValue 
         => new ExceptionResponse(new { code = "resource_not_found", reason = $"{apiIntegrationException.Message}" },
                (HttpStatusCode) apiIntegrationException.StatusCode ),
        ApiIntegrationException apiIntegrationException when !apiIntegrationException.StatusCode.HasValue
        => new ExceptionResponse(new { code = "resource_not_found", reason = $"{apiIntegrationException.Message}" }, HttpStatusCode.BadRequest),

        _ => new ExceptionResponse(new { code = "error", reason = "Internal Server Error" },
               HttpStatusCode.InternalServerError)
    };
}
