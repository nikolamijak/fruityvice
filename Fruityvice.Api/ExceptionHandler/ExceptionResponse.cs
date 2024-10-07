using System.Net;

namespace Fruityvice.Api.ExceptionHandler;

public record ExceptionResponse(object Response, HttpStatusCode StatusCode);
