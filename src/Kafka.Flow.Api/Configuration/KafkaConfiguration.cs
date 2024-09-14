using Kafka.Flow.Api.Producers;
using KafkaFlow;
using KafkaFlow.Serializer;

namespace Kafka.Flow.Api.Configuration;

public static class KafkaConfiguration
{
    public static void ConfigureKafka(this IServiceCollection services)
    {
        services.AddSingleton<ISampleMessageProducer, SampleMessageProducer>();
        const string topicName = "example-topic";

        services.AddKafka(
            kafka => kafka
                .UseConsoleLog()
                .AddCluster(
                    cluster => cluster
                        .WithBrokers(["localhost:7030"])
                        .CreateTopicIfNotExists(topicName, numberOfPartitions: 1, replicationFactor: 1)
                        .AddProducer<SampleMessageProducer>(
                            builder => builder
                                .DefaultTopic(topicName)
                                .AddMiddlewares(
                                    middleware => middleware
                                        .AddSerializer<JsonCoreSerializer>())
                        )
                )
        );
    }
}