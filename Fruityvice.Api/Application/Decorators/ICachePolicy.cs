using MediatR;

namespace Fruityvice.Api.Application.Decorators;

public interface ICachePolicy<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    TimeSpan Expiration => TimeSpan.FromMinutes(5);

    string GetCacheKey(TRequest request)
    {
        var r = new { request };
        var props = r.request.GetType().GetProperties().Select(pi => $"{pi.Name}:{pi.GetValue(r.request, null)}");
        return $"{typeof(TRequest).FullName}{{{String.Join(",", props)}}}";
    }
}
