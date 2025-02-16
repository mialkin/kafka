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
        using var producer = new ProducerBuilder<Null, UserCreatedEvent>(_producerConfig).Build();

        var deliveryResult = await producer.ProduceAsync(
            topic: "simple-producer-topic",
            message: new Message<Null, UserCreatedEvent> { Value = @event },
            cancellationToken: cancellationToken);

        logger.LogInformation(
            "Delivered {Value} to topic partition offset {TopicPartitionOffset}",
            deliveryResult.Value, deliveryResult.TopicPartitionOffset);
    }
}