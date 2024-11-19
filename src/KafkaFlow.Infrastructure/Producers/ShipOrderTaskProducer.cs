using KafkaFlow.Infrastructure.Models;
using Microsoft.Extensions.Logging;

namespace KafkaFlow.Infrastructure.Producers;

public class ShipOrderTaskProducer(
    IMessageProducer<ShipOrderTask> messageProducer,
    ILogger<ShipOrderTaskProducer> logger) :
    IShipOrderTaskProducer
{
    public async Task ProduceAsync(ShipOrderTask shipOrderTask)
    {
        logger.LogInformation("Producing message to Kafka. Message: {@Message}", shipOrderTask);
        await messageProducer.ProduceAsync(messageKey: null, messageValue: shipOrderTask);
        logger.LogInformation("Successfully produced message to Kafka. Message: {@Message}", shipOrderTask);
    }
}