
using Fruityvice.Api.Application.Models.Contracts;

namespace Fruityvice.Api.Application.Models.ApiResponse
{
    public record FruitResponse(Fruit Fruit, Metadata Metadata);

    public record Metadata(IDictionary<string,string> metadata);
    
}
