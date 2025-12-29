using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Carter;
using Catalog.API.Data;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));

});
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddCarter();

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();
if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogSeedInitial>();
}
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

var app = builder.Build();

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter=UIResponseWriter.WriteHealthCheckUIResponse
    });
app.UseExceptionHandler(exceptionHandlerApp =>
{
});
app.MapCarter();

app.Run();
