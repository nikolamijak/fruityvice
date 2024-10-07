using Fruityvice.Api.Application.Models.ApiResponse;
using Fruityvice.Api.Application.Ports;
using MediatR;

namespace Fruityvice.Api.Application.Queries.GetFruit;

public class GetFruitedHandler(IFruitApiService fruitApiService, IFruitMetadataRepository fruitMetadataRepository) : IRequestHandler<GetFruiteRequest, FruitResponse>
{
    public async Task<FruitResponse> Handle(GetFruiteRequest request, CancellationToken cancellationToken)
    {
        var fromApi = await fruitApiService.GetFruitByNameAsync(request.Name); 
        var fromRepo = await fruitMetadataRepository.GetMetadataAsync(request.Name);
        Metadata metadata = new(fromRepo);
        return new FruitResponse(fromApi, metadata);
    }
}
