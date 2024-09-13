using Kafka.Confluent.Api.Producers;

namespace Kafka.Confluent.Api.Configuration;

public static class KafkaProducersConfiguration
{
    public static void ConfigureKafkaProducers(this IServiceCollection services)
    {
        services.AddSingleton<SimpleProducer>();
    }
}