using Ardalis.GuardClauses;
using Kafka.Confluent.Infrastructure.Producers;
using Kafka.Confluent.Infrastructure.Settings;

namespace Kafka.Confluent.Api.Configurations;

public static class KafkaConfiguration
{
    public static void ConfigureKafka(this IServiceCollection services, IConfiguration configuration)
    {
        var configurationSection = configuration.GetRequiredSection(nameof(KafkaProducerSettings));
        var settings = configurationSection.Get<KafkaProducerSettings>();

        Guard.Against.Null(settings, nameof(KafkaProducerSettings));
        Guard.Against.NullOrWhiteSpace(settings.BootstrapServers);

        services.Configure<KafkaProducerSettings>(configurationSection);

        services.AddSingleton<UserCreatedEventProducer>();
    }
}