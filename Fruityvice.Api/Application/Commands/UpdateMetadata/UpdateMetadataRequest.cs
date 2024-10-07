using Fruityvice.Api.Application.Commands.Base;
using Fruityvice.Api.Application.Models.ApiResponse;
using MediatR;

namespace Fruityvice.Api.Application.Commands.UpdateMetadata;
public record UpdateMetadataRequest : MetaDataCommand, IRequest<FruitResponse>
{
    public UpdateMetadataRequest(FruitCommand Fruit) : base(Fruit)
    {
    }
}
