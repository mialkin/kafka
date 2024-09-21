using KafkaFlow;
using KafkaFlow.Api.Configurations;
using KafkaFlow.Infrastructure.Models;
using KafkaFlow.Infrastructure.Producers;
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

services.ConfigureApplication(builder.Configuration);

var application = builder.Build();

application.UseSerilogRequestLogging();
application.MapGet("/", () => "KafkaFlow.Api");

application.MapGet("/produce", async ([FromServices] IShipOrderTaskProducer messageProducer, string orderNumber) =>
{
    await messageProducer.ProduceAsync(new ShipOrderTask { OrderNumber = orderNumber });
    return Results.Ok();
});

var kafkaBus = application.Services.CreateKafkaBus();
await kafkaBus.StartAsync();

application.Run();