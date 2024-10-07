using FluentValidation.Results;
using FluentValidation;

namespace Fruityvice.Api.Application.Exceptions;

public class CustomValidationException : ValidationException
{
    private const string DefaultMessage = "One or more validation failures have occurred.";

    public string Code { get; } = "validation_error";

    public string ErrorMessage { get; }

    public IDictionary<string, string[]> Failures { get; private set; }
    public CustomValidationException() : this(DefaultMessage)
    {
    }

    public CustomValidationException(string message) : base(message)
    {
        ErrorMessage = message;
    }

    public CustomValidationException(IEnumerable<ValidationFailure> failures)
        : this(DefaultMessage, failures)
    {
    }

    public CustomValidationException(string message, IEnumerable<ValidationFailure> failures)
        : base(message, failures)
    {
        Failures = new Dictionary<string, string[]>();
        var propertyNames = failures
            .Select(e => e.PropertyName)
            .ToArray()
            .Distinct();

        foreach (var propertyName in propertyNames)
        {
            var propertyFailures = failures
                .Where(e => e.PropertyName == propertyName)
                .Select(e => e.ErrorMessage)
                .Distinct()
                .ToArray();

            if (!Failures.ContainsKey(propertyName))
            {
                Failures.Add(propertyName, propertyFailures);
            }
        }
    }
}