using Confluent.Kafka;

namespace Kafka.Confluent.Api.Producers;

public class SimpleProducer
{
    private readonly ILogger<SimpleProducer> _logger;
    private readonly ProducerConfig _producerConfig;

    public SimpleProducer(ILogger<SimpleProducer> logger)
    {
        _logger = logger;
        _producerConfig = new ProducerConfig
        {
            BootstrapServers = "localhost:7030",
            BrokerAddressFamily = BrokerAddressFamily.V4
        };
    }

    public async Task ProduceAsync(string message)
    {
        // If serializers are not specified, default serializers from
        // `Confluent.Kafka.Serializers` will be automatically used where
        // available. Note: by default strings are encoded as UTF8.
        using var producer = new ProducerBuilder<Null, string>(_producerConfig).Build();

        try
        {
            var deliveryResult = await producer.ProduceAsync(
                topic: "simple-producer-topic",
                message: new Message<Null, string> { Value = message });

            _logger.LogInformation(
                "Delivered {Value} to topic partition offset {TopicPartitionOffset}",
                deliveryResult.Value, deliveryResult.TopicPartitionOffset);
        }
        catch (ProduceException<Null, string> exception)
        {
            _logger.LogError(exception, "Unexpected exception occured");
        }
    }
}