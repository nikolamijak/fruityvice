using Fruityvice.Api.ApiGroups;
using Fruityvice.Api.Application.Decorators;
using Fruityvice.Api.ExceptionHandler;
using Fruityvice.Api.Infrastructure.Cache;
using Fruityvice.Api.Infrastructure.Http;
using Fruityvice.Api.Infrastructure.Persistance;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// register dependencies
// fluent style
builder.Services
    .AddFruityViceHttpClient()
    .AddInMemoryMetadataRepository()
    .AddExceptionHandler()
    .AddMediator()
    .RegisterAllValidators()
    .AddValidationDecorator()
    .RegisterCachePolicies()
    .AddInMemoryCache()
    .AddInMemoryCacheDecorator();
    
// eof custom dependencies


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler(exh => { });

// register Api Group (instead of controllers)
app.MapFruitsApi();

app.Run();


