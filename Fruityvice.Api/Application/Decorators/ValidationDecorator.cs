using FluentValidation;
using Fruityvice.Api.Application.Exceptions;
using MediatR;
using System.Diagnostics;

namespace Fruityvice.Api.Application.Decorators
{
    public class ValidationDecorator<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
            where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (validators != null && validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var results = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context)));
                var failures = results.SelectMany(result => result.Errors)
                       .Where(f => f != null)
                       .Distinct();
                if (failures.Any())
                {
                    // Enrich validation errors!

                    var tagsCollection = new ActivityTagsCollection();
                    foreach (var failure in failures)
                    {
                        if (!tagsCollection.ContainsKey($"{failure?.PropertyName}"))
                            tagsCollection.Add($"{failure?.PropertyName}", failure?.ErrorMessage);
                    }

                    throw new CustomValidationException(failures);
                }
            }
            return await next().ConfigureAwait(false);
        }
    }
}
