using Confluent.Kafka;

namespace KafkaFlow.Api.Configuration;

public record KafkaProducerConfig
{
    public required string ProducerName { get; set; }
    public required string Name { get; set; }
    public required string Topic { get; set; }
    public required ProducerConfig ProducerConfig { get; set; }
}