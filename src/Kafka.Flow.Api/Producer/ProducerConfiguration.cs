using KafkaFlow;
using KafkaFlow.Serializer;

namespace Kafka.Flow.Api.Producer;

public static class ProducerConfiguration
{
    public static void ConfigureKafka(this IServiceCollection services)
    {
        services.AddSingleton<IMessageProducer, MessageProducer>();
        const string topicName = "example-topic";

        services.AddKafka(
            kafka => kafka
                .UseConsoleLog()
                .AddCluster(
                    cluster => cluster
                        .WithBrokers(["localhost:7030"])
                        .CreateTopicIfNotExists(topicName, numberOfPartitions: 1, replicationFactor: 1)
                        .AddProducer<MessageProducer>(x =>
                            x.DefaultTopic(topicName)
                                .AddMiddlewares(y => y.AddSerializer<JsonCoreSerializer>())
                        )
                )
        );
    }
}