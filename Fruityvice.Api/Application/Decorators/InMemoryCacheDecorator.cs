using Fruityvice.Api.Application.Ports;
using MediatR;

namespace Fruityvice.Api.Application.Decorators
{
    public class InMemoryCacheDecorator<TRequest, TResponse>
        (
        ICacheService cache,
        IEnumerable<ICachePolicy<TRequest, TResponse>> cachePolicies
        ) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var cachePolicy = cachePolicies.FirstOrDefault();
            if (cachePolicy == null)
            {
                // No cache policy found, so just continue through the pipeline
                return await next();
            }
            var cacheKey = cachePolicy.GetCacheKey(request);
            var cachedResponse = await cache.GetAsync<TResponse>(cacheKey);
            if (cachedResponse != null)
            {
                return cachedResponse;
            }
            var response = await next();
            await cache.SetAsync(cacheKey, response, cachePolicy.Expiration);
            return response;
        }

    }
}
