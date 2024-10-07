using Fruityvice.Api.Application.Models;
using Fruityvice.Api.Application.Models.Contracts;

namespace Fruityvice.Api.Application.Ports;

public interface IFruitApiService
{
    Task<Fruit> GetFruitByNameAsync(string name);
}
