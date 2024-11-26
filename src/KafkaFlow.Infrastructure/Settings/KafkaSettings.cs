namespace KafkaFlow.Infrastructure.Settings;

public class KafkaSettings
{
    public required string[] Brokers { get; set; }
    public List<KafkaConsumerSettings>? Consumers { get; set; }
    public List<KafkaProducerSettings>? Producers { get; set; }

    public KafkaConsumerSettings GetConsumerSettings(string consumerName)
    {
        if (string.IsNullOrWhiteSpace(consumerName))
        {
            throw new ArgumentException("Empty Kafka consumer name");
        }

        if (Consumers is null)
        {
            throw new InvalidOperationException($"Kafka consumer '{consumerName}' is not set");
        }

        return Consumers.Single(x => x.Name == consumerName);
    }

    public KafkaProducerSettings GetProducerSettings(string producerName)
    {
        if (string.IsNullOrWhiteSpace(producerName))
        {
            throw new ArgumentException("Empty Kafka producer name");
        }

        if (Producers is null)
        {
            throw new InvalidOperationException($"Kafka producer '{producerName}' is not set");
        }

        return Producers.Single(x => x.Name == producerName);
    }
}