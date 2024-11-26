using KafkaFlow.Infrastructure.Models;

namespace KafkaFlow.Infrastructure.Producers;

public interface IShipOrderTaskProducer
{
    Task ProduceAsync(ShipOrderTaskResult shipOrderTaskResult);
}