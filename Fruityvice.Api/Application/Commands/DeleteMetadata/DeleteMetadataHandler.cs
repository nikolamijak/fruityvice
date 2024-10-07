using Fruityvice.Api.Application.Exceptions;
using Fruityvice.Api.Application.Models.ApiResponse;
using Fruityvice.Api.Application.Ports;
using Fruityvice.Api.Application.Queries.GetFruit;
using MediatR;

namespace Fruityvice.Api.Application.Commands.DeleteMetadata;

public class DeleteMetadataHandler
    (
    IFruitApiService fruitApiService, 
    IFruitMetadataRepository fruitMetadataRepository,
    ICacheService cacheService
    ) : IRequestHandler<DeleteMetadataRequest, FruitResponse>
{
    public async Task<FruitResponse> Handle(DeleteMetadataRequest request, CancellationToken cancellationToken)
    {
        var fromApi = await fruitApiService.GetFruitByNameAsync(request.Fruit.Name);
        var fromRepo = await fruitMetadataRepository.GetMetadataAsync(request.Fruit.Name);
        if (!fromRepo.TryGetValue(request.Fruit.Metadata.Key, out string? metadataValue))
        {
            throw new CustomValidationException($"Metadata with key {request.Fruit.Metadata.Key} does't exist.");
        }
        var metadata = await fruitMetadataRepository.DeleteMetadataAsync(request.Fruit.Name, request.Fruit.Metadata.Key);
        var cacheKey = new GetFruitCachePolicy().GetCacheKey(new GetFruiteRequest(request.Fruit.Name));
        await cacheService.InvalidateAsync(cacheKey);
        return new FruitResponse(fromApi, new(metadata));
    }
}
