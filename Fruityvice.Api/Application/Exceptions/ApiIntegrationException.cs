namespace Fruityvice.Api.Application.Exceptions;

public class ApiIntegrationException : Exception
{
    private const string DefaultMessage = "Exceptoin with the endpoint";

    public int? StatusCode { get; } = null;

    public ApiIntegrationException() : this(DefaultMessage)
    {
    }

    public ApiIntegrationException(string message) : base(message)
    {
    }

    public ApiIntegrationException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public ApiIntegrationException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    //protected ApiIntegrationException(SerializationInfo info, StreamingContext context) : base(info, context)
    //{
    //}
}
