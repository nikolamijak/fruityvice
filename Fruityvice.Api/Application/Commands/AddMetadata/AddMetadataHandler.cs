using Fruityvice.Api.Application.Exceptions;
using Fruityvice.Api.Application.Models.ApiResponse;
using Fruityvice.Api.Application.Ports;
using Fruityvice.Api.Application.Queries.GetFruit;
using MediatR;

namespace Fruityvice.Api.Application.Commands.AddMetadata;

public class AddMetadataHandler
    (
    IFruitApiService fruitApiService, 
    IFruitMetadataRepository fruitMetadataRepository,
    ICacheService cacheService
    ) : IRequestHandler<AddMetadataRequest, FruitResponse>
{
    public async Task<FruitResponse> Handle(AddMetadataRequest request, CancellationToken cancellationToken)
    {
        var fromApi = await fruitApiService.GetFruitByNameAsync(request.Fruit.Name);
        var fromRepo = await fruitMetadataRepository.GetMetadataAsync(request.Fruit.Name);
        if(fromRepo.TryGetValue(request.Fruit.Metadata.Key, out string? metadataValue)) {
            throw new CustomValidationException($"Metadata key {request.Fruit.Metadata.Key} exists with value {metadataValue ?? ""}");
        }
        // check if metadata exists
        var metadata = await fruitMetadataRepository.AddMetadataAsync(request.Fruit.Name, request.Fruit.Metadata.Key, request.Fruit.Metadata.Value!);
        var cacheKey = new GetFruitCachePolicy().GetCacheKey(new GetFruiteRequest(request.Fruit.Name));
        await cacheService.InvalidateAsync(cacheKey);
        return new FruitResponse(fromApi, new(metadata));
    }
}