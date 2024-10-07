namespace Fruityvice.Api.Application.Commands.Base;

public record FruitCommand(string Name, MetadataRequest Metadata);

public record MetadataRequest(string Key, string? Value);
public record MetaDataCommand (FruitCommand Fruit);
