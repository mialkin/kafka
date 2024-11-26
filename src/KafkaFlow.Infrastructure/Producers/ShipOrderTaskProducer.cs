using KafkaFlow.Infrastructure.Models;
using Microsoft.Extensions.Logging;

namespace KafkaFlow.Infrastructure.Producers;

public class ShipOrderTaskProducer(
    IMessageProducer<ShipOrderTaskResult> messageProducer,
    ILogger<ShipOrderTaskProducer> logger) :
    IShipOrderTaskProducer
{
    public async Task ProduceAsync(ShipOrderTaskResult shipOrderTaskResult)
    {
        logger.LogInformation("Producing message to Kafka. Message: {@Message}", shipOrderTaskResult);
        await messageProducer.ProduceAsync(messageKey: null, messageValue: shipOrderTaskResult);
        logger.LogInformation("Successfully produced message to Kafka. Message: {@Message}", shipOrderTaskResult);
    }
}