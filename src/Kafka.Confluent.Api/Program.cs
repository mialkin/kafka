using Bogus;
using Kafka.Confluent.Infrastructure.Configurations;
using Kafka.Confluent.Infrastructure.Producers;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
    configuration.WriteTo.Console();
});

services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options => { options.DescribeAllParametersInCamelCase(); });

services.AddRouting(options => options.LowercaseUrls = true);

services.ConfigureKafka();

var application = builder.Build();

application.UseSerilogRequestLogging();

application.UseSwagger();
application.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

application.MapGet("produce-random-message", async (SimpleProducer simpleProducer) =>
{
    Faker faker = new("ru");
    await simpleProducer.ProduceAsync(faker.Random.Word());

    return Results.Ok();
});

application.Run();