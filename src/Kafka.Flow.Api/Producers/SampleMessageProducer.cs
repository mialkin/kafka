using KafkaFlow;

namespace Kafka.Flow.Api.Producers;

public class SampleMessageProducer(IMessageProducer<SampleMessageProducer> messageProducer) : ISampleMessageProducer
{
    public async Task ProduceAsync(SampleMessage sampleMessage)
    {
        await messageProducer.ProduceAsync(null, sampleMessage);
    }
}

public interface ISampleMessageProducer
{
    Task ProduceAsync(SampleMessage sampleMessage);
}