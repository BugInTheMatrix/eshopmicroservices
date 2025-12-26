using BuildingBlocks.Behaviors;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddCarter();

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();

app.UseExceptionHandler(exceptionHandlerApp =>
{
});
app.MapCarter();

app.Run();
