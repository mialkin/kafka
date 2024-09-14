using Serilog;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
    configuration.WriteTo.Console();
});

services.AddRouting(options => options.LowercaseUrls = true);

var application = builder.Build();

application.UseSerilogRequestLogging();
application.MapGet("/", () => "Kafka.Flow.Api");
application.MapGet("/produce", (string message) =>
{
    return $"Produced message: \"{message}\"";
});


application.Run();