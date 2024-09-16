namespace Kafka.Flow.Api.Configuration;

public record KafkaConfig
{
    public required string[] Brokers { get; set; }
    public List<KafkaConsumerConfig> Consumers { get; set; } = [];
    public List<KafkaProducerConfig> Producers { get; set; } = [];

    public KafkaConsumerConfig GetConsumerConfig(string name)
    {
        return Consumers.FirstOrDefault(x => x.Name == name)
               ?? throw new InvalidOperationException($"Missing Kafka consumer settings for name \"{name}\"");
    }

    public KafkaProducerConfig GetProducerConfig(string name)
    {
        return Producers.FirstOrDefault(x => x.Name == name)
               ?? throw new InvalidOperationException($"Missing Kafka producer settings for name \"{name}\"");
    }
}