using Kafka.Confluent.Infrastructure.Producers;
using Microsoft.Extensions.DependencyInjection;

namespace Kafka.Confluent.Infrastructure.Configurations;

public static class KafkaConfiguration
{
    public static void ConfigureKafka(this IServiceCollection services)
    {
        services.AddSingleton<UserCreatedEventProducer>();
    }
}