using Kafka.Flow.Api.Models;
using KafkaFlow;

namespace Kafka.Flow.Api.Consumers;

public class HelloMessageHandler(ILogger<HelloMessageHandler> logger) : IMessageHandler<HelloMessage>
{
    public Task Handle(IMessageContext context, HelloMessage message)
    {
        logger.LogInformation(
            "Partition: {Partition} | Offset: {Offset} | Message: {Text}",
            context.ConsumerContext.Partition,
            context.ConsumerContext.Offset,
            message.Text);

        return Task.CompletedTask;
    }
}