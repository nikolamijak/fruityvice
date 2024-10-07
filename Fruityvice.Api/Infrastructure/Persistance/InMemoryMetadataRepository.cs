using Fruityvice.Api.Application.Ports;

namespace Fruityvice.Api.Infrastructure.Persistance;

public class InMemoryMetadataRepository : IFruitMetadataRepository
{
    private readonly Dictionary<string, Dictionary<string, string>> _fruitMetadata = new();
    public async Task<Dictionary<string, string>> AddMetadataAsync(string fruitName, string key, string value)
    {
        if (!_fruitMetadata.ContainsKey(fruitName))
        {
            _fruitMetadata[fruitName] = new Dictionary<string, string>();
        }
        _fruitMetadata[fruitName][key] = value;
        var metadata = await GetMetadataAsync(fruitName);
        return metadata;
    }

    public async Task<Dictionary<string, string>> DeleteMetadataAsync(string fruitName, string key)
    {
        _fruitMetadata[fruitName].Remove(key);
        var metadata = await GetMetadataAsync(fruitName);
        return  metadata;
    }

    public Task<Dictionary<string, string>> GetMetadataAsync(string fruitName)
    {
        if (_fruitMetadata.TryGetValue(fruitName, out var metadata))
        {
            return Task.FromResult(metadata ?? new Dictionary<string, string>());
        }
        return Task.FromResult(new Dictionary<string, string>()); // Return empty metadata if none exists
    }

    public async Task<Dictionary<string, string>> UpdateMetadataAsync(string fruitName, string key, string value)
    {            
        _fruitMetadata[fruitName][key] = value;
        var metadata = await GetMetadataAsync(fruitName);
        return metadata;
    }
}
