using System.Text;
using KafkaFlow.Infrastructure.Models;
using Microsoft.Extensions.Logging;

namespace KafkaFlow.Infrastructure.Producers;

public class ShipOrderTaskProducer(
    IMessageProducer<MessageTypeA> messageProducer,
    ILogger<ShipOrderTaskProducer> logger) :
    IShipOrderTaskProducer
{
    public async Task ProduceAsync(MessageTypeA messageTypeA)
    {
        logger.LogInformation("Producing message to Kafka. Message: {@Message}", messageTypeA);
        await messageProducer.ProduceAsync(
            messageKey: Guid.NewGuid().ToString(),
            messageValue: messageTypeA,
            headers: new MessageHeaders
            {
                {
                    "guid", Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())
                }
            }
        );
        logger.LogInformation("Successfully produced message to Kafka. Message: {@Message}", messageTypeA);
    }
}