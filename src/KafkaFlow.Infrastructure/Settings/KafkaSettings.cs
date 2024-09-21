namespace KafkaFlow.Infrastructure.Settings;

public class KafkaSettings
{
    public required string[] Brokers { get; set; }
    public List<KafkaConsumerSettings>? Consumers { get; set; }

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
}