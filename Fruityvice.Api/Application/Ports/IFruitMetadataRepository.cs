using Fruityvice.Api.Application.Models;

namespace Fruityvice.Api.Application.Ports;

public interface IFruitMetadataRepository
{
    Task<Dictionary<string, string>> AddMetadataAsync(string fruitName, string key, string value);

    Task<Dictionary<string, string>> UpdateMetadataAsync(string fruitName, string key, string value);

    Task<Dictionary<string, string>> DeleteMetadataAsync(string fruitName, string key);
    Task<Dictionary<string, string>> GetMetadataAsync(string fruitName);
}
