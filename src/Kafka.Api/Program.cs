using Kafka.Api.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
    configuration.WriteTo.Console();
});

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options => { options.DescribeAllParametersInCamelCase(); });

services.AddRouting(options => options.LowercaseUrls = true);

services.ConfigureKafkaProducers();

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.MapControllers();

app.Run();