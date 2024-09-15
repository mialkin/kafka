using Kafka.Flow.Api.Consumers;
using Kafka.Flow.Api.Producers;
using KafkaFlow;
using KafkaFlow.Serializer;

namespace Kafka.Flow.Api.Configuration;

public static class KafkaConfiguration
{
    public static void ConfigureKafka(this IServiceCollection services)
    {
        services.AddSingleton<IHelloMessageProducer, HelloMessageProducer>();
        const string topicName = "example-topic";

        services.AddKafka(
            kafka => kafka
                .UseConsoleLog()
                .AddCluster(
                    cluster => cluster
                        .WithBrokers(["localhost:7030"])
                        .CreateTopicIfNotExists(topicName, numberOfPartitions: 1, replicationFactor: 1)
                        .AddProducer<HelloMessageProducer>(
                            builder => builder
                                .DefaultTopic(topicName)
                                .AddMiddlewares(
                                    middleware => middleware
                                        .AddSerializer<JsonCoreSerializer>())
                        )
                        .AddConsumer(consumer => consumer
                            .Topic(topicName)
                            .WithGroupId("example-group")
                            .WithBufferSize(1)
                            .WithWorkersCount(1)
                            .AddMiddlewares(middlewares => middlewares
                                .AddDeserializer<JsonCoreDeserializer>()
                                .AddTypedHandlers(h => h.AddHandler<HelloMessageHandler>())
                            )
                        )
                )
        );
    }
}