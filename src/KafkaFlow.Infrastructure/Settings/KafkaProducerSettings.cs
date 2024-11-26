using Confluent.Kafka;

namespace KafkaFlow.Infrastructure.Settings;

public class KafkaProducerSettings
{
    public required string Name { get; init; }
    public required string Topic { get; init; }
    public ProducerConfig? Configuration { get; init; }
}