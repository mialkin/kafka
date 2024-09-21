using Confluent.Kafka;

namespace KafkaFlow.Api.Configuration;

public record KafkaConsumerConfig
{
    public required string Name { get; set; }
    public required string TopicName { get; set; }
    public required ConsumerConfig ConsumerConfig { get; set; }
}