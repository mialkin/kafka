using Confluent.Kafka;

namespace KafkaFlow.Infrastructure.Settings;

public record KafkaConsumerSettings
{
    public required string Name { get; set; }
    public required string Topic { get; set; }
    public required int WorkersCount { get; set; }
    public required int BufferSize { get; set; }
    public required ConsumerConfig ConsumerConfig { get; set; }
}