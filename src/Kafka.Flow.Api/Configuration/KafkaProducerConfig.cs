using Confluent.Kafka;

namespace Kafka.Flow.Api.Configuration;

public record KafkaProducerConfig
{
    public required string ProducerName { get; set; }
    public required string Name { get; set; }
    public required string Topic { get; set; }
    public required ProducerConfig ProducerConfig { get; set; }
}