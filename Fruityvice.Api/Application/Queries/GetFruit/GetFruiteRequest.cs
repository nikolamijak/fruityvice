using Fruityvice.Api.Application.Models.ApiResponse;
using MediatR;

namespace Fruityvice.Api.Application.Queries.GetFruit;

public record GetFruiteRequest(string Name) : IRequest<FruitResponse>;    
