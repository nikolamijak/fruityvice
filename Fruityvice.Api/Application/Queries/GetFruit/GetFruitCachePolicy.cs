using Fruityvice.Api.Application.Decorators;
using Fruityvice.Api.Application.Models.ApiResponse;
using MediatR;

namespace Fruityvice.Api.Application.Queries.GetFruit;
public class GetFruitCachePolicy : ICachePolicy<GetFruiteRequest, FruitResponse>
{
    // Cache it for 1 hour
    public TimeSpan Expiration => TimeSpan.FromHours(1);

    public string GetCacheKey(GetFruiteRequest request)
    {
        return request.ToString();
    }
}
