using KafkaFlow.Infrastructure.Models;
using Microsoft.Extensions.Logging;

namespace KafkaFlow.Infrastructure.Consumers;

public class ShipOrderTaskHandler(ILogger<ShipOrderTaskHandler> logger) : IMessageHandler<MessageTypeB>
{
    public Task Handle(IMessageContext context, MessageTypeB message)
    {
        logger.LogInformation(
            "Consumed new ship order task. Order number: {OrderNumber}. Partition: {Partition}. Offset: {Offset}.",
            message.OrderNumber,
            context.ConsumerContext.Partition,
            context.ConsumerContext.Offset);

        return Task.CompletedTask;
    }
}