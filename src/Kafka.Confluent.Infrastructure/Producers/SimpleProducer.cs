using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace Kafka.Confluent.Infrastructure.Producers;

public class SimpleProducer(ILogger<SimpleProducer> logger)
{
    private readonly ProducerConfig _producerConfig = new()
    {
        BootstrapServers = "localhost:9092",
        BrokerAddressFamily = BrokerAddressFamily.V4
    };

    public async Task ProduceAsync(string message)
    {
        // If serializers are not specified, default serializers from
        // `Confluent.Kafka.Serializers` will be automatically used where
        // available. Note: by default strings are encoded as UTF8.
        using var producer = new ProducerBuilder<Null, string>(_producerConfig).Build();

        var deliveryResult = await producer.ProduceAsync(
            topic: "simple-producer-topic",
            message: new Message<Null, string> { Value = message });

        logger.LogInformation(
            "Delivered {Value} to topic partition offset {TopicPartitionOffset}",
            deliveryResult.Value, deliveryResult.TopicPartitionOffset);
    }
}