using Kafka.Flow.Api.Producer;
using Microsoft.AspNetCore.Mvc;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
    configuration.WriteTo.Console();
});

services.AddRouting(options => options.LowercaseUrls = true);
services.ConfigureKafka();

var application = builder.Build();

application.UseSerilogRequestLogging();
application.MapGet("/", () => "Kafka.Flow.Api");

application.MapGet("/produce", async ([FromServices] IMessageProducer messageProducer, string message) =>
{
    await messageProducer.ProduceAsync(new Message { Text = message });
    return Results.Ok();
});

application.Run();