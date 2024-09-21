using KafkaFlow.Infrastructure.Models;

namespace KafkaFlow.Infrastructure.Producers;

public class ShipOrderTaskProducer(IMessageProducer<ShipOrderTaskProducer> messageProducer) :
    IShipOrderTaskProducer
{
    public async Task ProduceAsync(ShipOrderTask shipOrderTask)
    {
        await messageProducer.ProduceAsync(null, shipOrderTask);
    }
}