using System.Text.Json;
using Confluent.Kafka;
using Kafka.Confluent.Infrastructure.Constants;
using Kafka.Confluent.Infrastructure.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kafka.Confluent.Infrastructure.Producers;

public class UserCreatedEventProducer(IOptions<KafkaProducerSettings> options, ILogger<UserCreatedEventProducer> logger)
{
    private readonly KafkaProducerSettings _producerSettings = options.Value;

    public async Task ProduceAsync(UserCreatedEvent @event, CancellationToken cancellationToken)
    {
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = _producerSettings.BootstrapServers,
            BrokerAddressFamily = BrokerAddressFamily.V4
        };

        // If serializers are not specified, default serializers from
        // `Confluent.Kafka.Serializers` will be automatically used where
        // available. Note: by default strings are encoded as UTF8.
        using var producer = new ProducerBuilder<string, string>(producerConfig).Build();

        var value = JsonSerializer.Serialize(@event);

        var deliveryResult = await producer.ProduceAsync(
            topic: TopicNames.UserCreated,
            message: new Message<string, string>
            {
                Key = @event.Id.ToString(),
                Value = value,
                Headers = [new Header(HeaderNames.MessageType, "UserCreated"u8.ToArray())]
            },
            cancellationToken: cancellationToken);

        logger.LogInformation(
            "Delivered {Value} to topic partition offset {TopicPartitionOffset}",
            deliveryResult.Value, deliveryResult.TopicPartitionOffset);
    }
}