using Kafka.Flow.Api.Configuration;
using Kafka.Flow.Api.Producers;
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

application.MapGet("/produce", async ([FromServices] ISampleMessageProducer messageProducer, string message) =>
{
    await messageProducer.ProduceAsync(new SampleMessage { Text = message });
    return Results.Ok();
});

application.Run();