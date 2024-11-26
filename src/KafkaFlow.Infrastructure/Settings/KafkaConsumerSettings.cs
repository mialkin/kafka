using Confluent.Kafka;

namespace KafkaFlow.Infrastructure.Settings;

public record KafkaConsumerSettings(
    string Name,
    string Topic,
    int WorkersCount,
    int BufferSize,
    ConsumerConfig Configuration);