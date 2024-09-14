using KafkaFlow;

namespace Kafka.Flow.Api.Producer;

public class MessageProducer(IMessageProducer<MessageProducer> messageProducer) : IMessageProducer
{
    public async Task ProduceAsync(Message message)
    {
        await messageProducer.ProduceAsync(null, message);
    }
}

public interface IMessageProducer
{
    Task ProduceAsync(Message message);
}