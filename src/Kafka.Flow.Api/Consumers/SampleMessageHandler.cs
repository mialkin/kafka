using Kafka.Flow.Api.Models;
using KafkaFlow;

namespace Kafka.Flow.Api.Consumers;

public class HelloMessageHandler(ILogger<HelloMessageHandler> logger) : IMessageHandler<HelloMessage>
{
    public Task Handle(IMessageContext context, HelloMessage message)
    {
        logger.LogInformation(
            "Consumed new message: {Message}. Partition: {Partition}. Offset: {Offset}.",
            message.Text,
            context.ConsumerContext.Partition,
            context.ConsumerContext.Offset);

        return Task.CompletedTask;
    }
}