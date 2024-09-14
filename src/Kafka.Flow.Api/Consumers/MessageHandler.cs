using Kafka.Flow.Api.Producers;
using KafkaFlow;

namespace Kafka.Flow.Api.Consumers;

public class MessageHandler(ILogger<MessageHandler> logger) : IMessageHandler<SampleMessage>
{
    public Task Handle(IMessageContext context, SampleMessage sampleMessage)
    {
        logger.LogInformation("Partition: {Partition} | Offset: {Offset} | Message: {Message}",
            context.ConsumerContext.Partition,
            context.ConsumerContext.Offset,
            sampleMessage.Text);
        
        return Task.CompletedTask;
    }
}