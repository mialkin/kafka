using KafkaFlow;
using KafkaFlow.Api.Consumers;
using KafkaFlow.Api.Producers;
using KafkaFlow.Serializer;

namespace KafkaFlow.Api.Configuration;

public static class KafkaConfiguration
{
    public static void ConfigureKafka(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IHelloMessageProducer, HelloMessageProducer>();

        var kafkaConfig = configuration.GetSection("Kafka").Get<KafkaConfig>();
        var consumerConfig = kafkaConfig!.GetConsumerConfig("example-consumer");

        services.AddKafka(
            kafka => kafka
                .UseConsoleLog()
                .AddCluster(
                    cluster => cluster
                        .WithBrokers(kafkaConfig.Brokers)
                        .CreateTopicIfNotExists("example-topic", numberOfPartitions: 1, replicationFactor: 1)
                        .AddProducer<HelloMessageProducer>(
                            builder => builder
                                .DefaultTopic(consumerConfig.TopicName)
                                .AddMiddlewares(
                                    middleware => middleware
                                        .AddSerializer<JsonCoreSerializer>())
                        )
                        .AddConsumer(consumer => consumer
                            .WithConsumerConfig(consumerConfig.ConsumerConfig)
                            .Topic(consumerConfig.TopicName)
                            .WithBufferSize(1)
                            .WithWorkersCount(1)
                            .AddMiddlewares(middlewares => middlewares
                                .AddDeserializer<JsonCoreDeserializer>()
                                .AddTypedHandlers(builder => builder.AddHandler<HelloMessageHandler>())
                            )
                        )
                )
        );
    }
}