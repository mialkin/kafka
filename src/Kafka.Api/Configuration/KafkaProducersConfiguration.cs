using Kafka.Api.Producers;
using Microsoft.Extensions.DependencyInjection;

namespace Kafka.Api.Configuration;

public static class KafkaProducersConfiguration
{
    public static void ConfigureKafkaProducers(this IServiceCollection services)
    {
        services.AddSingleton<SimpleProducer>();
    }
}