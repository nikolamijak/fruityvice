namespace Fruityvice.Api.ExceptionHandler;

public interface IExceptionToResponseMapper
{
    ExceptionResponse MapExceptionToResponse(Exception exception);
}
