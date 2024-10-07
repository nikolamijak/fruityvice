using Fruityvice.Api.Application.Commands.AddMetadata;
using Fruityvice.Api.Application.Commands.Base;
using Fruityvice.Api.Application.Commands.DeleteMetadata;
using Fruityvice.Api.Application.Commands.UpdateMetadata;
using Fruityvice.Api.Application.Models.ApiResponse;
using Fruityvice.Api.Application.Queries.GetFruit;
using Fruityvice.Api.ExceptionHandler;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fruityvice.Api.ApiGroups;

public static class Fruits
{
    public static IEndpointRouteBuilder MapFruitsApi(this IEndpointRouteBuilder app)
    {
        var fruitApiGroup = app.MapGroup("/fruit")
            .WithName("Fruit")
            .WithOpenApi();


        fruitApiGroup.MapGet("/{name}", async ([AsParameters] GetFruiteRequest request, IMediator mediator) =>
          {
              var result = await mediator.Send(request);
              return Results.Ok(result);
          })
           .Produces<FruitResponse>(StatusCodes.Status200OK)
           .Produces<ExceptionResponse>(StatusCodes.Status404NotFound)
           .Produces<ExceptionResponse>(StatusCodes.Status500InternalServerError)
           .Produces<ExceptionResponse>(StatusCodes.Status400BadRequest);


        fruitApiGroup.MapPost("/{name}/metadata", async (string name, [FromBody] MetadataRequest metadata, IMediator mediator) =>
        {
            var result = await mediator.Send(new AddMetadataRequest(new FruitCommand(name, metadata)));
            return Results.Ok(result);
        })
       .WithName("add-metadata")
       .Produces<FruitResponse>(StatusCodes.Status200OK)
       .Produces<ExceptionResponse>(StatusCodes.Status404NotFound)
       .Produces<ExceptionResponse>(StatusCodes.Status500InternalServerError)
       .Produces<ExceptionResponse>(StatusCodes.Status400BadRequest);


        fruitApiGroup.MapPut("/fruit/{name}/metadata", async (string name, [FromBody] MetadataRequest metadata, IMediator mediator) =>
        {
            var result = await mediator.Send(new UpdateMetadataRequest(new FruitCommand(name, metadata)));
            return Results.Ok(result);
        })
       .WithName("update-metadata")
       .Produces<FruitResponse>(StatusCodes.Status200OK)
       .Produces<ExceptionResponse>(StatusCodes.Status404NotFound)
       .Produces<ExceptionResponse>(StatusCodes.Status500InternalServerError)
       .Produces<ExceptionResponse>(StatusCodes.Status400BadRequest);

        fruitApiGroup.MapDelete("/fruit/{name}/metadata", async (string name, [FromBody] MetadataRequest metadata, IMediator mediator) =>
        {
            var result = await mediator.Send(new DeleteMetadataRequest(new FruitCommand(name, metadata)));
            return Results.Ok(result);
        })
      .WithName("delete-metadata")
      .Produces<FruitResponse>(StatusCodes.Status200OK)
      .Produces<ExceptionResponse>(StatusCodes.Status404NotFound)
      .Produces<ExceptionResponse>(StatusCodes.Status500InternalServerError)
      .Produces<ExceptionResponse>(StatusCodes.Status400BadRequest);


       return app;

    }
}
