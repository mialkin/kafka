using KafkaFlow.Infrastructure.Configurations;
using KafkaFlow.Infrastructure.Settings;

namespace KafkaFlow.Api.Configurations;

public static class ApplicationConfiguration
{
    public static void ConfigureApplication(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        var kafkaSettings = configurationManager.GetRequiredSection(key: nameof(KafkaSettings)).Get<KafkaSettings>();
        services.ConfigureKafka(kafkaSettings!);
    }
}