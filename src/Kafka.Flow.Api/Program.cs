using Kafka.Flow.Api.Configuration;
using Kafka.Flow.Api.Models;
using Kafka.Flow.Api.Producers;
using KafkaFlow;
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

application.MapGet("/produce", async ([FromServices] IHelloMessageProducer messageProducer, string message) =>
{
    await messageProducer.ProduceAsync(new HelloMessage { Text = message });
    return Results.Ok();
});

var kafkaBus = application.Services.CreateKafkaBus();
await kafkaBus.StartAsync();

application.Run();