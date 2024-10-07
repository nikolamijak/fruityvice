using Fruityvice.Api.Application.Commands.Base;
using Fruityvice.Api.Application.Models.ApiResponse;
using MediatR;

namespace Fruityvice.Api.Application.Commands.DeleteMetadata;
public record DeleteMetadataRequest : MetaDataCommand, IRequest<FruitResponse>
{
    public DeleteMetadataRequest(FruitCommand Fruit) : base(Fruit)
    {
    }
}
