using KafkaFlow;
using KafkaFlow.Api.Models;

namespace KafkaFlow.Api.Producers;

public class HelloMessageProducer(IMessageProducer<HelloMessageProducer> messageProducer) : IHelloMessageProducer
{
    public async Task ProduceAsync(HelloMessage helloMessage)
    {
        await messageProducer.ProduceAsync(null, helloMessage);
    }
}

public interface IHelloMessageProducer
{
    Task ProduceAsync(HelloMessage helloMessage);
}