using Fruityvice.Api.Application.Commands.Base;
using Fruityvice.Api.Application.Models.ApiResponse;
using MediatR;

namespace Fruityvice.Api.Application.Commands.AddMetadata;

public record AddMetadataRequest : MetaDataCommand, IRequest<FruitResponse>
{
    public AddMetadataRequest(FruitCommand Fruit) : base(Fruit)
    {
    }
}
