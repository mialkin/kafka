using KafkaFlow.Infrastructure.Models;

namespace KafkaFlow.Infrastructure.Producers;

public interface IShipOrderTaskProducer
{
    Task ProduceAsync(MessageTypeA messageTypeA);
}