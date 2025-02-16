using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace Kafka.Confluent.Infrastructure.Producers;

public class UserCreatedEventProducer(ILogger<UserCreatedEventProducer> logger)
{
    private readonly ProducerConfig _producerConfig = new()
    {
        BootstrapServers = "localhost:9092",
        BrokerAddressFamily = BrokerAddressFamily.V4
    };

    public async Task ProduceAsync(UserCreatedEvent @event, CancellationToken cancellationToken)
    {
        // If serializers are not specified, default serializers from
        // `Confluent.Kafka.Serializers` will be automatically used where
        // available. Note: by default strings are encoded as UTF8.
        using var producer = new ProducerBuilder<string, string>(_producerConfig).Build();

        var value = JsonSerializer.Serialize(@event);

        var deliveryResult = await producer.ProduceAsync(
            topic: "simple-producer-topic",
            message: new Message<string, string>
            {
                Key = @event.Id.ToString(),
                Value = value,
                Headers = [new Header("Message-Type", "UserCreated"u8.ToArray())]
            },
            cancellationToken: cancellationToken);

        logger.LogInformation(
            "Delivered {Value} to topic partition offset {TopicPartitionOffset}",
            deliveryResult.Value, deliveryResult.TopicPartitionOffset);
    }
}